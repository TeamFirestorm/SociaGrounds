using System.Collections.Generic;
using System.Threading;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using SociaGroundsEngine.PlayerFolder;

namespace SociaGroundsEngine.Multiplayer
{
    public class PlayersSendClient
    {
        private static NetClient _clientGame;
        private static NetQueue<List<CPlayer>> _locations;

        public static async void Setup()
        {
            NetPeerConfiguration config = new NetPeerConfiguration("game") {AutoFlushSendQueue = false};
            _clientGame = new NetClient(config);
        }

        // called by the UI
        public static NetOutgoingMessage CreateLocation(CPlayer player)
        {
            NetOutgoingMessage om = _clientGame.CreateMessage();
            om.Write((byte)PacketTypes.CONNECT);
            om.Write(player.Position.X);
            om.Write(player.Position.Y);
            return om;
        }

        // called by the UI
        public static void Connect(string host, int port)
        {
            _clientGame.Start();

            NetOutgoingMessage hail = CreateLocation(Game1.players[0]);
            _clientGame.Connect(host, port, hail);
        }

        // called by the UI
        public static void SendLocation()
        {
            NetOutgoingMessage om = CreateLocation(Game1.players[0]);
            _clientGame.SendMessage(om, NetDeliveryMethod.ReliableOrdered);
            _clientGame.FlushSendQueue();
        }

        public static void GotMessage(object peer)
        {
            NetIncomingMessage im;
            while ((im = _clientGame.ReadMessage()) != null)
            {
                // handle incoming message
                switch (im.MessageType)
                {
                    case NetIncomingMessageType.DebugMessage:
                    case NetIncomingMessageType.ErrorMessage:
                    case NetIncomingMessageType.WarningMessage:
                    case NetIncomingMessageType.VerboseDebugMessage:
                        string error = im.ReadString();
                        break;

                    case NetIncomingMessageType.Data:

                        if (im.ReadByte() == (byte) PacketTypes.WORLDSTATE)
                        {
                            _locations.Clear();

                            int count = 0;

                            // Read int
                            count = im.ReadInt32();

                            for (int i = 0; i < count; i++)
                            {
                                
                            }
                        }
                        break;

                    case NetIncomingMessageType.StatusChanged:
                        NetConnectionStatus status = (NetConnectionStatus)im.ReadByte();

                        if (status == NetConnectionStatus.Connected)
                        {
                            //Console.WriteLine("Disconnect");
                        }

                        if (status == NetConnectionStatus.Disconnected)
                        {
                            //Console.WriteLine("Connect");
                        }

                        string reason = im.ReadString();
                        break;

                    default:
                        //("Unhandled type: " + im.MessageType + " " + im.LengthBytes + " bytes");
                        break;
                }
                _clientGame.Recycle(im);
            }
        }

        public static void DisConnect()
        {
            _clientGame.Disconnect("Requested by user");
        }

        // called by the UI
        public static void Shutdown()
        {
            _clientGame.Shutdown("Requested by user");
        }
    }
}
