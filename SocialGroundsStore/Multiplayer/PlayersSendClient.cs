using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SocialGroundsStore.PlayerFolder;

namespace SocialGroundsStore.Multiplayer
{
    public class PlayersSendClient
    {
        // Client Object
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
            // Read ip to string
            string hostip = ip;
            // Create new instance of configs. Parameter is "application Id". It has to be same on client and server.
            NetPeerConfiguration config = new NetPeerConfiguration("game");

            //Players
            Game1.players.Add(new MyPlayer(new Vector2(0, 0), content.Load<Texture2D>("Personas/Chris_Character")));

            // Create new client, with previously created configs
            _client = new NetClient(config);
            _client.Start();

            // Create new outgoing message
            NetOutgoingMessage outmsg = _client.CreateMessage();

            // Write byte ( first byte informs server about the message type ) ( This way we know, what kind of variables to read )
            outmsg.Write((byte)PacketTypes.Connect);
            outmsg.Write(Game1.players[0].Position.X);
            outmsg.Write(Game1.players[0].Position.Y);

            // Connect client, to ip previously requested from user
            _client.Connect(hostip, 14242, outmsg);

            // Funtion that waits for connection approval info from server
            WaitForStartingInfo();
        }

        // Before main looping starts, we loop here and wait for approval message
        private void WaitForStartingInfo()
        {
            // When this is set to true, we are approved and ready to go
            bool canStart = false;

            // Loop untill we are approved
            while (!canStart)
            {
                // New incoming message // If new messages arrived
                NetIncomingMessage msg = _client.ReadMessage();
                if ((msg != null))
                {
                    byte firstPackage = msg.ReadByte();

                    // Switch based on the message types
                    switch (msg.MessageType)
                    {
                        case NetIncomingMessageType.DebugMessage:
                        case NetIncomingMessageType.ErrorMessage:
                        case NetIncomingMessageType.Error:
                        case NetIncomingMessageType.VerboseDebugMessage:
                        case NetIncomingMessageType.WarningMessage:
                            break;

                        // All manually sent messages are type of "Data"
                        case NetIncomingMessageType.Data:

                            // Read the first byte
                            // This way we can separate packets from each others
                            if (firstPackage == (byte)PacketTypes.Connect)
                            {
                                Game1.players[0].Id = msg.ReadInt32();
                                int numPlayers = msg.ReadInt32();

                                for (int i = 0; i < numPlayers; i++)
                                {
                                    int id = msg.ReadInt32();
                                    float x = msg.ReadFloat();
                                    float y = msg.ReadFloat();

                                    Game1.players.Add(new ForeignPlayer(new Vector2(x,y), id));
                                }

                                // When all players are added to list, start the game
                                _watch.Start();
                                canStart = true;
                                _started = true;
                            }
                            break;
                    }                                
                }
            }
        }

        /// <summary>
        /// Check for new incoming messages from server
        /// </summary>
#pragma warning disable 1998
        private async static void CheckServerMessages()
#pragma warning restore 1998
        {
            // Create new incoming message holder
            NetIncomingMessage msg;

            // While theres new messages
            // THIS is exactly the same as in WaitForStartingInfo() function
            // Check if its Data message
            // If its WorldState, read all the characters to list
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
                        int id = msg.ReadInt32();

                        foreach (CPlayer player in Game1.players)
                        {
                            if (player.GetType() != typeof(ForeignPlayer)) continue;

                            ForeignPlayer foreign = (ForeignPlayer)player;

                            if (foreign.Id != id) continue;

                            float x = msg.ReadFloat();
                            float y = msg.ReadFloat();

                            foreign.AddNewPosition(new Vector2(x, y));
                        }
                    }
                    else if (firstPackage == (byte)PacketTypes.Disconnect)
                    {
                        int id = msg.ReadInt32();

                        foreach (CPlayer player in Game1.players)
                        {
                            if (player.GetType() != typeof(ForeignPlayer)) continue;

                            ForeignPlayer foreign = (ForeignPlayer)player;

                            if (foreign.Id == id)
                            {
                                Game1.players.Remove(foreign);
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

                CheckServerMessages();
                if (_watch.ElapsedMilliseconds >= Game1.sendTime)
                {
                    _watch.Restart();
                    // Check if server sent new messages
                    GetInputAndSendItToServer(Game1.players[0].Position);
                }
            }
        }

        // Get input from player and send it to server
        private static void GetInputAndSendItToServer(Vector2 newPosition)
        {
            // Create new message
            NetOutgoingMessage outmsg = _client.CreateMessage();

            // Write byte = Set "MOVE" as packet type
            outmsg.Write((byte)PacketTypes.Move);

            // Write byte = move direction
            outmsg.Write(Game1.players[0].Id);
            outmsg.Write(newPosition.X);
            outmsg.Write(newPosition.Y);

            // Send it to server
            _client.SendMessage(outmsg, NetDeliveryMethod.ReliableOrdered);
        }
    }
}
