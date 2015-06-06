using System;
using Windows.UI.Xaml;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using SociaGroundsEngine.PlayerFolder;

namespace SociaGroundsEngine.Multiplayer
{
    public class PlayersSendClient
    {
        // Client Object
        private static NetClient Client;

        // Create timer that tells client, when to send update
        private static DispatcherTimer update;

        // Indicates if program is running
        private static bool IsRunning = true;

        public PlayersSendClient(string ip)
        {
            // Read Ip to string
            string hostip = ip;
            // Create new instance of configs. Parameter is "application Id". It has to be same on client and server.
            NetPeerConfiguration config = new NetPeerConfiguration("game");
            config.Port = 14242;

            // Create new client, with previously created configs
            Client = new NetClient(config);

            // Create new outgoing message
            NetOutgoingMessage outmsg = Client.CreateMessage();

            // Start client
            Client.Start();

            // Write byte ( first byte informs server about the message type ) ( This way we know, what kind of variables to read )
            outmsg.Write((byte) PacketTypes.Connect);
            outmsg.Write(Game1.players[0].Position.X);
            outmsg.Write(Game1.players[0].Position.Y);

            // Connect client, to ip previously requested from user 
            Client.Connect(hostip, 14242, outmsg);

            update = new DispatcherTimer {Interval = new TimeSpan(0, 0, 0, 1)};

            // When time has elapsed ( 50ms in this case ), call "update_Elapsed" funtion
            update.Tick += update_Elapsed;

            // Funtion that waits for connection approval info from server
            WaitForStartingInfo();

            // Start the timer
            update.Start();

            // While..running
            while (IsRunning)
            {
                // Just loop this like madman
                GetInputAndSendItToServer(Game1.players[0].Position);
            }
        }

        private void update_Elapsed(object sender, object e)
        {
            // Check if server sent new messages
            CheckServerMessages();
        }

        // Before main looping starts, we loop here and wait for approval message
        private static void WaitForStartingInfo()
        {
            // When this is set to true, we are approved and ready to go
            bool canStart = false;

            // New incomgin message
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

                            // Read the first byte
                            // This way we can separate packets from each others
                            if (im.ReadByte() == (byte) PacketTypes.WorldState)
                            {
                                // Worldstate packet structure
                                //
                                // int = count of players
                                // character obj * count

                                //Console.WriteLine("WorldState Update");

                                // Declare count
                                int count = 0;

                                // Read int
                                count = im.ReadInt32();

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

                                // When all players are added to list, start the game
                                canStart = true;
                            }
                            break;

                        default:
                            // Should not happen and if happens, don't care
                            break;
                    }
                }
            }
        }


        /// <summary>
        /// Check for new incoming messages from server
        /// </summary>
        private async static void CheckServerMessages()
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
                    if (inc.ReadByte() == (byte) PacketTypes.WorldState)
                    {
                        int index = inc.ReadInt32();

                        int x = inc.ReadInt32();
                        int y = inc.ReadInt32();

                        Game1.players[index].NewPosition = new Vector2(x, y);
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
            outmsg.Write((byte) PacketTypes.Move);

            // Write byte = move direction
            outmsg.Write(newPosition.X);
            outmsg.Write(newPosition.Y);

            // Send it to server
            Client.SendMessage(outmsg, NetDeliveryMethod.ReliableOrdered);
        }
    }
}
