using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SociaGrounds.Model.Controllers;
using SociaGrounds.Model.Players;

namespace SociaGrounds.Model.Multiplayer
{
    /// <summary>
    /// This class is used by the host to send data to the clients
    /// </summary>
    public class PlayersSendClient
    {
        // Host Object
        private static NetClient _client;

        private bool _started;
        private bool _stopped;
        private readonly Stopwatch _watch;

        public PlayersSendClient(ContentManager content, string ip)
        {
            _watch = new Stopwatch();
            _started = false;
            _stopped = false;
            CreateClient(content, ip);
        }

        private void CreateClient(ContentManager content, string ip)
        {
            // Read the ip and make it a string
            string hostip = ip;
            // Create new object of configs.
            NetPeerConfiguration config = new NetPeerConfiguration("game");

            //Players
            Static.Players.Add(new MyPlayer(new Vector2(0, 0), content.Load<Texture2D>("SociaGrounds/Personas/Chris_Character")));

            // Create new client with the defined settings
            _client = new NetClient(config);
            _client.Start();

            // Create new outgoing message
            NetOutgoingMessage outmsg = _client.CreateMessage();

            // Write byte, first byte describes the message type, letting the application know what to do with it.
            outmsg.Write((byte)PacketTypes.Connect);
            outmsg.Write(Static.Players[0].Position.X);
            outmsg.Write(Static.Players[0].Position.Y);

            // Connect client with the host IP and default port
            _client.Connect(hostip, 14242, outmsg);

            // Task that waits for the approval of the host
            Task.Run(new Action(WaitForStartingInfo));
        }

        // Method that waits for the approval message before starting the connection
        private void WaitForStartingInfo()
        {
            // When canStart is enabled, the game can start
            bool canStart = false;

            // Wait for approval
            while (!canStart)
            {
                // Read new messages
                NetIncomingMessage msg = _client.ReadMessage();
                if ((msg != null))
                {
                    //Switch that determines the message type
                    switch (msg.MessageType)
                    {
                        case NetIncomingMessageType.DebugMessage:
                        case NetIncomingMessageType.ErrorMessage:
                        case NetIncomingMessageType.Error:
                        case NetIncomingMessageType.VerboseDebugMessage:
                        case NetIncomingMessageType.WarningMessage:
                            break;


                        case NetIncomingMessageType.Data:
                            // Read the first byte
                            if (msg.ReadByte() == (byte)PacketTypes.Connect)
                            {
                                Static.Players[0].Id = msg.ReadInt32();
                                int numPlayers = msg.ReadInt32();

                                for (int i = 0; i < numPlayers; i++)
                                {
                                    int id = msg.ReadInt32();
                                    float x = msg.ReadFloat();
                                    float y = msg.ReadFloat();

                                    Static.Players.Add(new ForeignPlayer(new Vector2(x, y), id));
                                }

                                // When all players are added to list, start the game
                                _started = true;
                                canStart = true;
                                _watch.Start();
                            }
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Check if there are new messages from the server
        /// </summary>
#pragma warning disable 1998
        private async void CheckServerMessages()
#pragma warning restore 1998
        {
            // Create a message holder
            NetIncomingMessage msg;

            //Check data messages, when found, add players to the list
            while ((msg = _client.ReadMessage()) != null)
            {
                switch (msg.MessageType)
                {
                    case NetIncomingMessageType.DebugMessage:
                    case NetIncomingMessageType.ErrorMessage:
                    case NetIncomingMessageType.Error:
                    case NetIncomingMessageType.VerboseDebugMessage:
                    case NetIncomingMessageType.WarningMessage:
                        break;

                    case NetIncomingMessageType.Data:

                        byte firstPackage = msg.ReadByte();

                        if (firstPackage == (byte)PacketTypes.Move)
                        {
                            if (!_started) return;

                            int id = msg.ReadInt32();

                            foreach (CPlayer player in Static.Players)
                            {
                                if (player.GetType() != typeof(ForeignPlayer)) continue;

                                if (player.Id != id) continue;

                                ForeignPlayer foreign = (ForeignPlayer)player;

                                float x = msg.ReadFloat();
                                float y = msg.ReadFloat();
                                string mes = msg.ReadString();

                                foreign.AddNewPosition(new Vector2(x, y));
                                foreign.ChatMessage = mes;
                            }
                        }
                        else if (firstPackage == (byte)PacketTypes.Disconnect)
                        {
                            int id = msg.ReadInt32();

                            foreach (CPlayer player in Static.Players)
                            {
                                if (player.GetType() != typeof(ForeignPlayer)) continue;

                                ForeignPlayer foreign = (ForeignPlayer)player;

                                if (foreign.Id == id)
                                {
                                    Static.Players.Remove(foreign);
                                    break;
                                }
                            }
                        }
                        break;
                }
            }
        }

        public void Loop()
        {
            while (!_stopped)
            {
                if (!_started) continue;
                if (_watch.ElapsedMilliseconds >= Game1.SendTime)
                {
                    _watch.Restart();
                    // Check if server sent new messages
                    GetInputAndSendItToServer(Static.Players[0]);
                    CheckServerMessages();
                }
            }
        }

        // Get input from player and send it to server
        private static void GetInputAndSendItToServer(CPlayer player)
        {
            // Create new message
            NetOutgoingMessage outmsg = _client.CreateMessage();

            // Write byte = Set "MOVE" as packet type
            outmsg.Write((byte)PacketTypes.Move);

            // Write byte = move direction
            outmsg.Write(player.Id);
            outmsg.Write(player.Position.X);
            outmsg.Write(player.Position.Y);
            outmsg.Write(player.ChatMessage);

            // Send it to server
            _client.SendMessage(outmsg, NetDeliveryMethod.ReliableOrdered);
        }
    }
}
