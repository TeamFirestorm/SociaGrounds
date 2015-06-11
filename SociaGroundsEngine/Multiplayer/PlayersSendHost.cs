using System.Collections.Generic;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using SociaGroundsEngine.PlayerFolder;

namespace SociaGroundsEngine.Multiplayer
{
    public class PlayersSendHost
    {
        // Server object
        private static NetServer _netServer;

        // Object that can be used to store and read messages
        private NetIncomingMessage inc;

        private readonly List<ForeignPlayer> gameWorldState;

        // Indicates if program is running
        private static bool isRunning;

        public PlayersSendHost()
        {
            // Create new instance of configs. Parameter is "application Id". It has to be same on client and server.
            NetPeerConfiguration config = new NetPeerConfiguration("game");

            // Set server port
            config.Port = 14242;

            // Max client amount
            config.MaximumConnections = 20;

            // Enable New messagetype. Explained later
            config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);

            // Create new server based on the configs just defined
            _netServer = new NetServer(config);

            isRunning = false;

            // Start it
            _netServer.Start();

            // Create list of "Characters" ( defined later in code ). This list holds the world state. Character positions
            gameWorldState = new List<ForeignPlayer>();

            Loop();
        }

        private void Loop()
        {
            isRunning = true;
            while (isRunning)
            {
                ServerRunning();
            }
        }

        private async void ServerRunning()
        {
            // Main loop
            // This kind of loop can't be made in XNA. In there, its basically same, but without while
            // Or maybe it could be while(new messages)
            // Server.ReadMessage() Returns new messages, that have not yet been read.
            // If "inc" is null -> ReadMessage returned null -> Its null, so dont do this :)
            if ((inc = _netServer.ReadMessage()) != null)
            {
                // Theres few different types of messages. To simplify this process, i left only 2 of em here
                switch (inc.MessageType)
                {
                    // If incoming message is Request for connection approval
                    // This is the very first packet/message that is sent from client
                    // Here you can do new player initialisation stuff
                    case NetIncomingMessageType.ConnectionApproval:

                        // Read the first byte of the packet
                        // ( Enums can be casted to bytes, so it be used to make bytes human readable )
                        if (inc.ReadByte() == (byte)PacketTypes.Connect)
                        {
                            // Approve clients connection ( Its sort of agreenment. "You can be my client and i will host you" )
                            inc.SenderConnection.Approve();

                            // Add new character to the game.
                            // It adds new player to the list and stores name, ( that was sent from the client )
                            // Random x, y and stores client IP+Port
                            int x = inc.ReadInt32();
                            int y = inc.ReadInt32();

                            gameWorldState.Add(new ForeignPlayer(new Vector2(x, y), inc.SenderConnection));

                            // Create message, that can be written and sent
                            NetOutgoingMessage outmsg = _netServer.CreateMessage();

                            // first we write byte
                            outmsg.Write((byte)PacketTypes.WorldState);

                            // then int
                            outmsg.Write(gameWorldState.Count);

                            // iterate trought every character ingame
                            foreach (ForeignPlayer ch in gameWorldState)
                            {
                                // This is handy method
                                // It writes all the properties of object to the packet
                                if (inc.SenderConnection != ch.Connection)
                                {
                                    outmsg.WriteAllProperties(ch);
                                }
                            }

                            // Now, packet contains:
                            // Byte = packet type
                            // Int = how many players there is in game
                            // character object * how many players is in game

                            // Send message/packet to all connections, in reliably order, channel 0
                            // Reliably means, that each packet arrives in same order they were sent. Its slower than unreliable, but easyest to understand

                            _netServer.SendMessage(outmsg, inc.SenderConnection, NetDeliveryMethod.ReliableOrdered, 0);
                        }

                        break;
                    // Data type is all messages manually sent from client
                    // ( Approval is automated process )
                    case NetIncomingMessageType.Data:

                        // Read first byte
                        if (inc.ReadByte() == (byte)PacketTypes.Move)
                        {
                            // Check who sent the message
                            // This way we know, what character belongs to message sender
                            foreach (ForeignPlayer player in gameWorldState)
                            {
                                // If stored connection ( check approved message. We stored ip+port there, to character obj )
                                // Find the correct character
                                if (player.Connection != inc.SenderConnection)
                                    continue;

                                // Read next byte
                                byte b = inc.ReadByte();

                                // Handle movement. This byte should correspond to some direction
                                int x = inc.ReadInt32();
                                int y = inc.ReadInt32();

                                player.Position = new Vector2(x, y);

                                // Create new message
                                NetOutgoingMessage outmsg = _netServer.CreateMessage();

                                // Write byte, that is type of world state
                                outmsg.Write((byte)PacketTypes.WorldState);
                                outmsg.Write(gameWorldState.IndexOf(player));
                                outmsg.Write(x);
                                outmsg.Write(y);

                                // Send messsage to clients except the sender ( All connections, in reliable order, channel 0)
                                List<NetConnection> all = _netServer.Connections;
                                all.Remove(inc.SenderConnection);

                                _netServer.SendMessage(outmsg, all, NetDeliveryMethod.ReliableOrdered, 0);
                                break;
                            }
                        }
                        break;
                    case NetIncomingMessageType.StatusChanged:
                        // In case status changed
                        // It can be one of these
                        // NetConnectionStatus.Connected;
                        // NetConnectionStatus.Connecting;
                        // NetConnectionStatus.Disconnected;
                        // NetConnectionStatus.Disconnecting;
                        // NetConnectionStatus.None;

                        // NOTE: Disconnecting and Disconnected are not instant unless client is shutdown with disconnect()
                        //Console.WriteLine(inc.SenderConnection.ToString() + " status changed. " + (NetConnectionStatus)inc.SenderConnection.Status);
                        if (inc.SenderConnection.Status == NetConnectionStatus.Disconnected || inc.SenderConnection.Status == NetConnectionStatus.Disconnecting)
                        {
                            // Find disconnected character and remove it
                            foreach (ForeignPlayer cha in gameWorldState)
                            {
                                if (cha.Connection == inc.SenderConnection)
                                {
                                    gameWorldState.Remove(cha);
                                    break;
                                }
                            }
                        }
                        break;
                } // If New messages
            }
        }
    }
}


