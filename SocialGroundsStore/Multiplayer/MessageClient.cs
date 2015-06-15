using System.Threading;
using Lidgren.Network;

namespace SocialGroundsStore.Multiplayer
{
    public class MessageClient
    {
        private static NetClient _clientMessage;

#pragma warning disable 1998
        public static async void Setup()
#pragma warning restore 1998
        {
            NetPeerConfiguration config = new NetPeerConfiguration("chat");
            config.AutoFlushSendQueue = false;
            _clientMessage = new NetClient(config);

            _clientMessage.RegisterReceivedCallback(new SendOrPostCallback(GotMessage));

            _clientMessage.Shutdown("Bye");
        }

        public static void GotMessage(object peer)
        {
            NetIncomingMessage im;
            while ((im = _clientMessage.ReadMessage()) != null)
            {
                // handle incoming message
                switch (im.MessageType)
                {
                    case NetIncomingMessageType.DebugMessage:
                    case NetIncomingMessageType.ErrorMessage:
                    case NetIncomingMessageType.WarningMessage:
                    case NetIncomingMessageType.VerboseDebugMessage:
                        string text = im.ReadString();
                        //Output(text);
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
                        //Output(status + ": " + reason);
                        break;

                    case NetIncomingMessageType.Data:
                        string chat = im.ReadString();
                        //Output(chat);
                        break;

                    default:
                        //Output("Unhandled type: " + im.MessageType + " " + im.LengthBytes + " bytes");
                        break;
                }
                _clientMessage.Recycle(im);
            }
        }

        // called by the UI
        public static void Connect(string host, int port)
        {
            _clientMessage.Start();
            NetOutgoingMessage hail = _clientMessage.CreateMessage("This is the hail message");
            _clientMessage.Connect(host, port, hail);
        }

        // called by the UI
        public static void Shutdown()
        {
            _clientMessage.Disconnect("Requested by user");
            // s_client.Shutdown("Requested by user");
        }

        // called by the UI
        public static void Send(string text)
        {
            NetOutgoingMessage om = _clientMessage.CreateMessage(text);
            _clientMessage.SendMessage(om, NetDeliveryMethod.ReliableOrdered);
            //Output("Sending '" + text + "'");
            _clientMessage.FlushSendQueue();
        }
    }
}
