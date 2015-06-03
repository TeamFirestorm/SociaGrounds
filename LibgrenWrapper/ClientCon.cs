using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading;
using Lidgren.Network;
namespace LibgrenWrapper
{
    public class ClientCon
    {
        private static NetClient _sClient;

        public static async void Setup()
        {
            NetPeerConfiguration config = new NetPeerConfiguration("chat");
            config.AutoFlushSendQueue = false;
            _sClient = new NetClient(config);

            _sClient.RegisterReceivedCallback(new SendOrPostCallback(GotMessage));

            _sClient.Shutdown("Bye");
        }

        private static void Output(string text)
        {
            Console.WriteLine(text);
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
                        string text = im.ReadString();
                        Output(text);
                        break;

                   case NetIncomingMessageType.StatusChanged:
                        NetConnectionStatus status = (NetConnectionStatus)im.ReadByte();

                        if (status == NetConnectionStatus.Connected)
                        {
                            Console.WriteLine("Connected a nother client");
                        }
                        else if (status == NetConnectionStatus.Disconnected)
                        {
                            Console.WriteLine("Disconnected a nother client");
                        }
                        else
                        {
                            Console.WriteLine("Random");
                        }

                        string reason = im.ReadString();
                        Output(status + ": " + reason);
                        break;

                    case NetIncomingMessageType.Data:
                        string chat = im.ReadString();
                        Output(chat);
                        break;

                    default:
                        Output("Unhandled type: " + im.MessageType + " " + im.LengthBytes + " bytes");
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
            _sClient.Shutdown("Requested by user");
        }

        // called by the UI
        public static void Send(string text)
        {
            NetOutgoingMessage om = _sClient.CreateMessage(text);
            _sClient.SendMessage(om, NetDeliveryMethod.ReliableOrdered);
            Output("Sending '" + text + "'");
            _sClient.FlushSendQueue();
        }
    }
}
