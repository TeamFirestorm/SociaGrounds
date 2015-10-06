using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using SociaGrounds.Model.Controllers;
using SociaGrounds.Model.Players;

namespace SociaGrounds.Model.Multiplayer
{
    /// <summary>
    /// This class is used by the clients to send data to the host
    /// </summary>
    public class PlayersSendHost
    {
        // Server object
        private static NetServer _netServer;

        // Object that can be used to store and read messages
        private NetIncomingMessage _incMsg;

        // Indicates if program is running
        private bool _isRunning;

        // Keep track of the amount of players
        private int _numberOfPlayers;


        private readonly Stopwatch _watch;

        private Vector2 _lastPosition;

        /// <summary>
        /// Constructor of the class, adding players and making a connection to the host
        /// </summary>
        public PlayersSendHost()
        {
            _numberOfPlayers = 1;

            _watch = new Stopwatch();

            // Create new instance of configs. Parameter is "application Id". It has to be same on client and server.
            NetPeerConfiguration config = new NetPeerConfiguration("game")
            {
                // Set server port
                Port = 14242,

                // Max client amount
                MaximumConnections = 20
            };

            _lastPosition = new Vector2(0, 0);

            // Enable New messagetype. Explained later
            config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);

            // Create new server based on the configs just defined
            _netServer = new NetServer(config);

            //checks if the server is running
            _isRunning = false;

            // Start it
            _netServer.Start();
        }

        /// <summary>
        /// Loop that keeps the game updated
        /// </summary>
        public void Loop()
        {
            _isRunning = true;
            while (_isRunning)
            {
                if (_watch.ElapsedMilliseconds >= Static.SendTime)
                {
                    _watch.Stop();
                    SendLocationToClients(StaticPlayer.MyPlayer);
                    _watch.Restart();
                }
                ServerRunning();
            }
        }

        // Get input from player and send it to server
        private void SendLocationToClients(Player player)
        {
            List<NetConnection> all = _netServer.Connections;
            if (all.Count <= 0) return;

            if (_lastPosition == player.Position) return;

            _lastPosition = player.Position;

            // Write byte = Set "MOVE" as packet type
            NetOutgoingMessage outMsg = _netServer.CreateMessage();
            outMsg.Write((byte)PacketTypes.Move);
            outMsg.Write(0); //id
            outMsg.Write(player.Position.X);
            outMsg.Write(player.Position.Y);
            outMsg.Write(player.ChatMessage);
            _netServer.SendMessage(outMsg, _netServer.Connections, NetDeliveryMethod.ReliableOrdered, 0);
        }

        private void ServerRunning()
        {
            // Main loop
            // Server.ReadMessage() This methods returns messages to the player that have not been read yet
            if ((_incMsg = _netServer.ReadMessage()) != null)
            {
                switch (_incMsg.MessageType)
                {
                    case NetIncomingMessageType.VerboseDebugMessage:
                    case NetIncomingMessageType.DebugMessage:
                    case NetIncomingMessageType.ErrorMessage:
                    case NetIncomingMessageType.Error:
                    case NetIncomingMessageType.WarningMessage:
                    case NetIncomingMessageType.UnconnectedData:
                    case NetIncomingMessageType.Receipt:
                    case NetIncomingMessageType.DiscoveryRequest:
                    case NetIncomingMessageType.DiscoveryResponse:
                    case NetIncomingMessageType.NatIntroductionSuccess:
                    case NetIncomingMessageType.ConnectionLatencyUpdated:
                        Debug.WriteLine("--{0}: {1}", _incMsg.Data, _incMsg.MessageType);
                        break;

                    //First message of the host arrives here. Making sure connectinon is established and player can be loaded.
                    case NetIncomingMessageType.ConnectionApproval:
                        ConnectionApproval();
                        break;

                    case NetIncomingMessageType.Data:
                        Data();
                        break;
                    case NetIncomingMessageType.StatusChanged:
                        StatusChanged();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// When the status changes,
        /// Status can be:
        /// NetConnectionStatus.Connected;
        /// NetConnectionStatus.Connecting;
        /// NetConnectionStatus.Disconnected;
        /// NetConnectionStatus.Disconnecting;
        /// NetConnectionStatus.None;
        /// </summary>
        private void StatusChanged()
        {
            if (_incMsg.SenderConnection.Status == NetConnectionStatus.Disconnected || _incMsg.SenderConnection.Status == NetConnectionStatus.Disconnecting)
            {
                // Loop through the player until inactive is found and remove
                foreach (ForeignPlayer foreign in StaticPlayer.ForeignPlayers)
                {
                    if (foreign.Connection != _incMsg.SenderConnection) continue;

                    NetOutgoingMessage outmsg = _netServer.CreateMessage();
                    outmsg.Write((byte) PacketTypes.Disconnect);
                    outmsg.Write(foreign.Id);

                    StaticPlayer.ForeignPlayers.Remove(foreign);

                    List<NetConnection> all = _netServer.Connections;
                    all.Remove(_incMsg.SenderConnection);

                    _netServer.SendMessage(outmsg, all, NetDeliveryMethod.ReliableOrdered, 0);
                    break;
                }
            }
        }

        private void Data()
        {
            // Read first byte
            if (_incMsg.ReadByte() == (byte) PacketTypes.Move)
            {
                int id = _incMsg.ReadInt32();
                float x = _incMsg.ReadFloat();
                float y = _incMsg.ReadFloat();
                string msg = _incMsg.ReadString();

                ForeignPlayer foreign = StaticPlayer.FindForeignPlayerById(id);
                foreign.AddNewPosition(new Vector2(x, y));
                foreign.ChatMessage = msg;

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
                    outmsg.Write(msg);

                    _netServer.SendMessage(outmsg, all, NetDeliveryMethod.ReliableOrdered, 0);
                }
            }
        }

        private void ConnectionApproval()
        {
            // Reads the first byte of the packet
            if (_incMsg.ReadByte() == (byte) PacketTypes.Connect)
            {
                _watch.Stop();
                // Agreement of host and client is made here.
                _incMsg.SenderConnection.Approve();

                // Add a new player on this location
                float x = _incMsg.ReadFloat();
                float y = _incMsg.ReadFloat();

                StaticPlayer.ForeignPlayers.Add(new ForeignPlayer(new Vector2(x, y), _incMsg.SenderConnection, _numberOfPlayers));
                _numberOfPlayers++;

                for (int i = 0; i < 10; i++)
                {
                    // Create a message to send and receive
                    NetOutgoingMessage outmsg = _netServer.CreateMessage();

                    // Write the bytes
                    outmsg.Write((byte) PacketTypes.Connect);
                    outmsg.Write(StaticPlayer.ForeignPlayers.Last().Id);

                    outmsg.Write(StaticPlayer.ForeignPlayers.Count - 1);

                    if (StaticPlayer.ForeignPlayers.Count - 1 > 0)
                    {
                        // Loop through every character in the game
                        foreach (ForeignPlayer player in StaticPlayer.ForeignPlayers)
                        {
                            // All properties of the packet are kept here to send out
                            if (_incMsg.SenderConnection != player.Connection)
                            {
                                outmsg.Write(player.Id);
                                outmsg.Write(player.Position.X);
                                outmsg.Write(player.Position.Y);
                                outmsg.Write(player.ChatMessage);
                            }
                        }
                    }

                    // Sends message to all players in chronological order
                    // Messages are send in a reliable way, meaning they'll arrive in the same way that they are send
                    _netServer.SendMessage(outmsg, _incMsg.SenderConnection, NetDeliveryMethod.ReliableOrdered, 0);
                }
                _watch.Restart();
            }
        }
    }
}


