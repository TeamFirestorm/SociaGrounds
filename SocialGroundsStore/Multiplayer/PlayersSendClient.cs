using System;
using System.Threading.Tasks;
using Windows.Security.Cryptography.Core;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SocialGroundsStore.PlayerFolder;
using System.Diagnostics;

namespace SocialGroundsStore.Multiplayer
{
    public class PlayersSendClient
    {
        // Client Object
        private static NetClient Client;

        private int count;
        private bool started;

        public PlayersSendClient(ContentManager content, string ip)
        {
            count = 0;
            CreateDit(content, ip);
            started = false;
        }

        private async void CreateDit(ContentManager content, string ip)
        {
            // Read ip to string
            string hostip = ip;
            // Create new instance of configs. Parameter is "application Id". It has to be same on client and server.
            NetPeerConfiguration config = new NetPeerConfiguration("game");

            //Players
            Game1.players.Add(new MyPlayer(new Vector2(0, 0), content.Load<Texture2D>("Personas/Chris_Character")));

            // Create new client, with previously created configs
            Client = new NetClient(config);
            Client.Start();

            // Create new outgoing message
            NetOutgoingMessage outmsg = Client.CreateMessage();

            // Write byte ( first byte informs server about the message type ) ( This way we know, what kind of variables to read )
            outmsg.Write((byte)PacketTypes.Connect);
            outmsg.Write(Game1.players[0].Position.X);
            outmsg.Write(Game1.players[0].Position.Y);

            // Connect client, to ip previously requested from user
            Client.Connect(hostip, 14242, outmsg);

            // Funtion that waits for connection approval info from server
            WaitForStartingInfo();
        }

        public async void StartLoop()
        {
            await Task.Run(new Action(Loop));
        }

        private void Loop()
        {
            while (true)
            {
                if (started)
                {
                    if (count++ >= 60)
                    {
                        count = 0;
                        // Check if server sent new messages
                        GetInputAndSendItToServer(Game1.players[0].Position);
                        CheckServerMessages();
                    }
                }
            }
        }

        // Before main looping starts, we loop here and wait for approval message
        private void WaitForStartingInfo()
        {
            // When this is set to true, we are approved and ready to go
            bool canStart = false;

            // New incoming message
            NetIncomingMessage im;

            // Loop untill we are approved
            while (!canStart)
            {
                // If new messages arrived
                if ((im = Client.ReadMessage()) != null)
                {
                    // Switch based on the message types
                    switch (im.MessageType)
                    {
                        // All manually sent messages are type of "Data"
                        case NetIncomingMessageType.Data:

                            byte firstPackage = im.ReadByte();

                            // Read the first byte
                            // This way we can separate packets from each others
                            if (firstPackage == (byte)PacketTypes.WorldState)
                            {
                                Game1.players[0].Id = im.ReadInt32();

                                // Read int
                                int count = im.ReadInt32();

                                if (count > 0)
                                {
                                    // Iterate all players
                                    for (int i = 0; i < count; i++)
                                    {
                                        // Create new character to hold the data
                                        ForeignPlayer player = new ForeignPlayer();

                                        // Read all properties ( Server writes characters all props, so now we can read em here. Easy )
                                        im.ReadAllProperties(player);

                                        // Add it to list
                                        Game1.players.Add(player);
                                    }
                                }

                                // When all players are added to list, start the game
                                canStart = true;
                                started = true;
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
            NetIncomingMessage inc;

            // While theres new messages
            // THIS is exactly the same as in WaitForStartingInfo() function
            // Check if its Data message
            // If its WorldState, read all the characters to list
            while ((inc = Client.ReadMessage()) != null)
            {
                if (inc.MessageType == NetIncomingMessageType.Data)
                {
                    byte firstPackage = inc.ReadByte();

                    if (inc.ReadByte() == (byte)PacketTypes.Move)
                    {
                        int id = inc.ReadInt32();

                        foreach (CPlayer player in Game1.players)
                        {
                            if (player.GetType() != typeof(ForeignPlayer)) continue;

                            ForeignPlayer foreign = (ForeignPlayer)player;

                            if (foreign.Id != id) continue;

                            int x = inc.ReadInt32();
                            int y = inc.ReadInt32();

                            foreign.AddNewPosition(new Vector2(x, y));
                        }
                    }
                    else if (firstPackage == (byte)PacketTypes.Disconnect)
                    {
                        int id = inc.ReadInt32();

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
                }
            }
        }

        // Get input from player and send it to server
        private static void GetInputAndSendItToServer(Vector2 newPosition)
        {
            // Create new message
            NetOutgoingMessage outmsg = Client.CreateMessage();

            // Write byte = Set "MOVE" as packet type
            outmsg.Write((byte)PacketTypes.Move);

            // Write byte = move direction
            outmsg.Write(newPosition.X);
            outmsg.Write(newPosition.Y);

            // Send it to server
            Client.SendMessage(outmsg, NetDeliveryMethod.ReliableOrdered);
        }
    }
}
