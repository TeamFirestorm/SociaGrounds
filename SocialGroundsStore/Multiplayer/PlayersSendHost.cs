using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SocialGroundsStore.PlayerFolder;

namespace SocialGroundsStore.Multiplayer
{
    public class PlayersSendHost
    {
        // Server object
        private static NetServer _netServer;

        // Object that can be used to store and read messages
        private NetIncomingMessage inc;

        // Indicates if program is running
        private static bool _isRunning;

        private int numberOfPlayers = 1;

        private readonly Stopwatch _watch;

        public PlayersSendHost(ContentManager content)
        {
            Game1.players.Add(new MyPlayer(new Vector2(0, 0), content.Load<Texture2D>("Personas/Chris_Character"),0));

            _watch = new Stopwatch();

            // Create new instance of configs. Parameter is "application Id". It has to be same on client and server.
            NetPeerConfiguration config = new NetPeerConfiguration("game")
            {
                // Set server port
                Port = 14242,

                // Max client amount
                MaximumConnections = 20
            };

            // Enable New messagetype. Explained later
            config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);

            // Create new server based on the configs just defined
            _netServer = new NetServer(config);

            //checks if the server is running
            _isRunning = false;

            // Start it
            _netServer.Start();
        }

        public async void StartLoop()
        {
            await Task.Run(new Action(Loop));
        }

        private void Loop()
        {
            _isRunning = true;
            while (_isRunning)
            {
                ServerRunning();
            }
        }

        private void ServerRunning()
        {
            // Main loop
            // This kind of loop can't be made in XNA. In there, its basically same, but without while
            // Or maybe it could be while(new messages)
            // Server.ReadMessage() Returns new messages, that have not yet been read.
            // If "inc" is null -> ReadMessage returned null -> Its null, so dont do this :)
            if ((inc = _netServer.ReadMessage()) != null)
            {
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

                            Game1.players.Add(new ForeignPlayer(new Vector2(x, y), inc.SenderConnection, numberOfPlayers));
                            numberOfPlayers++;

                            // Create message, that can be written and sent
                            NetOutgoingMessage outmsg = _netServer.CreateMessage();

                            // first we write byte
                            outmsg.Write((byte)PacketTypes.WorldState);
                            outmsg.Write(Game1.players.Last().Id);
                            outmsg.Write(Game1.players.Count -1);

                            if (Game1.players.Count - 1 > 0)
                            {
                                // iterate trought every character ingame
                                foreach (CPlayer player in Game1.players)
                                {
                                    // This is handy method
                                    // It writes all the properties of object to the packet
                                    if (inc.SenderConnection != player.Connection)
                                    {
                                        outmsg.WriteAllProperties(player);
                                    }
                                }
                            }
                            // Send message/packet to all connections, in reliably order, channel 0
                            // Reliably means, that each packet arrives in same order they were sent. Its slower than unreliable, but easyest to understand

                            _netServer.SendMessage(outmsg, inc.SenderConnection, NetDeliveryMethod.ReliableOrdered, 0);

                            _watch.Restart();
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
                            foreach (CPlayer player in Game1.players)
                            {
                                if (player.GetType() != typeof(ForeignPlayer)) continue;

                                ForeignPlayer foreign = (ForeignPlayer)player;

                                // If stored connection ( check approved message. We stored ip+port there, to character obj )
                                // Find the correct character
                                if (foreign.Connection != inc.SenderConnection) continue;

                                // Read next byte
                                //byte b = inc.ReadByte();

                                // Handle movement. This byte should correspond to some direction
                                int x = inc.ReadInt32();
                                int y = inc.ReadInt32();

                                foreign.AddNewPosition(new Vector2(x,y));

                                // Create new message
                                NetOutgoingMessage outmsg = _netServer.CreateMessage();

                                // Write byte, that is type of world state
                                outmsg.Write((byte)PacketTypes.Move);
                                outmsg.Write(foreign.Id);
                                outmsg.Write(x);
                                outmsg.Write(y);

                                // Send messsage to clients except the sender ( All connections, in reliable order, channel 0)
                                List<NetConnection> all = _netServer.Connections;
                                all.Remove(inc.SenderConnection);

                                _netServer.SendMessage(outmsg, all, NetDeliveryMethod.ReliableOrdered, 0);

                                NetOutgoingMessage newMessage = _netServer.CreateMessage();

                                if (_watch.ElapsedMilliseconds > 3000)
                                {
                                    _watch.Restart();
                                    // Write byte, that is type of world state
                                    newMessage.Write((byte)PacketTypes.Move);
                                    newMessage.Write(0);
                                    newMessage.Write(Game1.players[0].Position.X);
                                    newMessage.Write(Game1.players[0].Position.Y);

                                    all = _netServer.Connections;

                                    _netServer.SendMessage(newMessage, all, NetDeliveryMethod.ReliableOrdered, 0);
                                }

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
                            foreach (CPlayer player in Game1.players)
                            {
                                if (player.GetType() != typeof(ForeignPlayer)) continue;

                                ForeignPlayer foreign = (ForeignPlayer)player;

                                if (foreign.Connection == inc.SenderConnection)
                                {
                                    NetOutgoingMessage outmsg = _netServer.CreateMessage();
                                    outmsg.Write((byte)PacketTypes.Disconnect);
                                    outmsg.Write(foreign.Id);

                                    Game1.players.Remove(foreign);

                                    List<NetConnection> all = _netServer.Connections;
                                    all.Remove(inc.SenderConnection);

                                    _netServer.SendMessage(outmsg, all, NetDeliveryMethod.ReliableOrdered, 0);
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


