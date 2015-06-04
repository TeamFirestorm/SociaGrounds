using System.Threading;
using Lidgren.Network;
using Microsoft.Xna.Framework;

namespace SociaGroundsEngine.Multiplayer
{
    public class PlayersSendClient
    {
        private static NetClient _sClient;

        public static async void Setup()
        {
            NetPeerConfiguration config = new NetPeerConfiguration("chat") {AutoFlushSendQueue = false};
            _sClient = new NetClient(config);

            _sClient.RegisterReceivedCallback(new SendOrPostCallback(GotMessage));

            _sClient.Shutdown("Bye");
        }

        // called by the UI
        public static void SendStart(string text)
        {
            NetOutgoingMessage om = _sClient.CreateMessage(text);
            _sClient.SendMessage(om, NetDeliveryMethod.ReliableOrdered);
            //Output("Sending '" + text + "'");
            _sClient.FlushSendQueue();
        }

        // called by the UI
        public static void SendLocation(Vector2 text)
        {
            NetOutgoingMessage om = _sClient.CreateMessage();
            _sClient.SendMessage(om, NetDeliveryMethod.ReliableOrdered);
            //Output("Sending '" + text + "'");
            _sClient.FlushSendQueue();
        }

        public static void GotMessage(object peer)
        {
            NetIncomingMessage im;
            while ((im = _sClient.ReadMessage()) != null)
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

                    case NetIncomingMessageType.Data:
                        string text = im.ReadString();
                        break;

                    default:
                        //("Unhandled type: " + im.MessageType + " " + im.LengthBytes + " bytes");
                        break;
                }
                _sClient.Recycle(im);
            }
        }

        // called by the UI
        public static void Connect(string host, int port)
        {
            _sClient.Start();
            NetOutgoingMessage hail = _sClient.CreateMessage("This is the hail message");
            _sClient.Connect(host, port, hail);
        }

        // called by the UI
        public static void Shutdown()
        {
            _sClient.Disconnect("Requested by user");
            // s_client.Shutdown("Requested by user");
        }

        // called by the UI
        public static void SendText(string text)
        {
            NetOutgoingMessage om = _sClient.CreateMessage(text);
            _sClient.SendMessage(om, NetDeliveryMethod.ReliableOrdered);
            //Output("Sending '" + text + "'");
            _sClient.FlushSendQueue();
        }
    }
}
