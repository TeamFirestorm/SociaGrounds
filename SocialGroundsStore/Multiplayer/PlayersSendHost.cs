using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        private NetIncomingMessage _incMsg;

        // Indicates if program is running
        private bool _isRunning;

        private int _numberOfPlayers;

        private readonly Stopwatch _watch;

        private Vector2 lastPosition;

        public PlayersSendHost(ContentManager content)
        {
            _numberOfPlayers = 1;

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

            lastPosition = new Vector2(0,0);

            // Enable New messagetype. Explained later
            config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);

            // Create new server based on the configs just defined
            _netServer = new NetServer(config);

            //checks if the server is running
            _isRunning = false;

            // Start it
            _netServer.Start();
        }

        public void Loop()
        {
            _isRunning = true;
            while (_isRunning)
            {
                if (_watch.ElapsedMilliseconds >= 3000)
                {
                    _watch.Restart();
                    SendLocationToClients();
                }
                ServerRunning();
            }
        }

        // Get input from player and send it to server
        private void SendLocationToClients()
        {
            List<NetConnection> all = _netServer.Connections;
            if (all.Count > 0)
            {
                if (lastPosition == Game1.players[0].Position) return;

                lastPosition = Game1.players[0].Position;

                // Write byte = Set "MOVE" as packet type
                NetOutgoingMessage outMsg = _netServer.CreateMessage();
                outMsg.Write((byte)PacketTypes.Move);
                outMsg.Write(0); //id
                outMsg.Write(Game1.players[0].Position.X);
                outMsg.Write(Game1.players[0].Position.Y);
                _netServer.SendMessage(outMsg, _netServer.Connections, NetDeliveryMethod.ReliableOrdered, 0);
            }         
        }

        private void ServerRunning()
        {
            // Main loop
            // This kind of loop can't be made in XNA. In there, its basically same, but without while
            // Or maybe it could be while(new messages)
            // Server.ReadMessage() Returns new messages, that have not yet been read.
            // If "inc" is null -> ReadMessage returned null -> Its null, so dont do this :)
            if ((_incMsg = _netServer.ReadMessage()) != null)
            {
                switch (_incMsg.MessageType)
                {
                    case NetIncomingMessageType.DebugMessage:
                    case NetIncomingMessageType.ErrorMessage:
                    case NetIncomingMessageType.Error:
                    case NetIncomingMessageType.VerboseDebugMessage:
                    case NetIncomingMessageType.WarningMessage:
                        break;

                    // If incoming message is Request for connection approval
                    // This is the very first packet/message that is sent from client
                    // Here you can do new player initialisation stuff
                    case NetIncomingMessageType.ConnectionApproval:

                        // Read the first byte of the packet
                        // ( Enums can be casted to bytes, so it be used to make bytes human readable )
                        if (_incMsg.ReadByte() == (byte)PacketTypes.Connect)
                        {
                            // Add new character to the game.
                            // It adds new player to the list and stores name, ( that was sent from the client )
                            // Random x, y and stores client IP+Port
                            float x = _incMsg.ReadFloat();
                            float y = _incMsg.ReadFloat();

                            Game1.players.Add(new ForeignPlayer(new Vector2(x, y), _incMsg.SenderConnection, _numberOfPlayers));
                            _numberOfPlayers++;

                            // Create message, that can be written and sent
                            NetOutgoingMessage outmsg = _netServer.CreateMessage();

                            // first we write byte
                            outmsg.Write((byte)PacketTypes.WorldState);
                            outmsg.Write(Game1.players.Last().Id);

                            outmsg.Write(Game1.players.Count - 1);

                            if (Game1.players.Count - 1 > 0)
                            {
                                // iterate trought every character ingame
                                foreach (CPlayer player in Game1.players)
                                {
                                    // This is handy method
                                    // It writes all the properties of object to the packet
                                    if (_incMsg.SenderConnection != player.Connection)
                                    {
                                        outmsg.Write(player.Id);
                                        outmsg.Write(player.Position.X);
                                        outmsg.Write(player.Position.Y);
                                    }
                                }
                            }

                            // Send message/packet to all connections, in reliably order, channel 0
                            // Reliably means, that each packet arrives in same order they were sent. Its slower than unreliable, but easyest to understand
                            _netServer.SendMessage(outmsg, _incMsg.SenderConnection, NetDeliveryMethod.ReliableOrdered, 0);

                            // Approve clients connection ( Its sort of agreenment. "You can be my client and i will host you" )
                            _incMsg.SenderConnection.Approve();

                            _watch.Restart();
                        }

                        break;
                    // Data type is all messages manually sent from client
                    // ( Approval is automated process )
                    case NetIncomingMessageType.Data:

                        // Read first byte
                        if (_incMsg.ReadByte() == (byte)PacketTypes.Move)
                        {
                            int id = _incMsg.ReadInt32();
                            float x = _incMsg.ReadFloat();
                            float y = _incMsg.ReadFloat();

                            ForeignPlayer foreign = (ForeignPlayer)Game1.CompareById(id);
                            foreign.AddNewPosition(new Vector2(x, y));

                            List<NetConnection> all = _netServer.Connections;
                            all.Remove(_incMsg.SenderConnection);

                            if (all.Count > 0)
                            {
                                NetOutgoingMessage outmsg = _netServer.CreateMessage();
                                // Write byte, that is type of world state
                                outmsg.Write((byte) PacketTypes.Move);
                                outmsg.Write(foreign.Id);
                                outmsg.Write(x);
                                outmsg.Write(y);

                                _netServer.SendMessage(outmsg, all, NetDeliveryMethod.ReliableOrdered, 0);
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
                        if (_incMsg.SenderConnection.Status == NetConnectionStatus.Disconnected || _incMsg.SenderConnection.Status == NetConnectionStatus.Disconnecting)
                        {
                            // Find disconnected character and remove it
                            foreach (CPlayer player in Game1.players)
                            {
                                if (player.GetType() != typeof(ForeignPlayer)) continue;

                                ForeignPlayer foreign = (ForeignPlayer)player;

                                if (foreign.Connection == _incMsg.SenderConnection)
                                {
                                    NetOutgoingMessage outmsg = _netServer.CreateMessage();
                                    outmsg.Write((byte)PacketTypes.Disconnect);
                                    outmsg.Write(foreign.Id);

                                    Game1.players.Remove(foreign);

                                    List<NetConnection> all = _netServer.Connections;
                                    all.Remove(_incMsg.SenderConnection);

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


