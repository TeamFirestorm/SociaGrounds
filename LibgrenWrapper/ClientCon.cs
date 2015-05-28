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
        private static NetClient s_client;

        public static async void Setup()
        {
            NetPeerConfiguration config = new NetPeerConfiguration("chat");
            config.AutoFlushSendQueue = false;
            s_client = new NetClient(config);

            s_client.RegisterReceivedCallback(new SendOrPostCallback(GotMessage));

            s_client.Shutdown("Bye");
        }

        private static void Output(string text)
        {
            Console.WriteLine(text);
        }

        public static void GotMessage(object peer)
        {
            NetIncomingMessage im;
            while ((im = s_client.ReadMessage()) != null)
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
                            Console.WriteLine("Disconnect");
                        }

                        if (status == NetConnectionStatus.Disconnected)
                        {
                            Console.WriteLine("Connect");
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
                s_client.Recycle(im);
            }
        }


        // called by the UI
        public static void Connect(string host, int port)
        {
            s_client.Start();
            NetOutgoingMessage hail = s_client.CreateMessage("This is the hail message");
            s_client.Connect(host, port, hail);
        }

        // called by the UI
        public static void Shutdown()
        {
            s_client.Disconnect("Requested by user");
            // s_client.Shutdown("Requested by user");
        }

        // called by the UI
        public static void Send(string text)
        {
            NetOutgoingMessage om = s_client.CreateMessage(text);
            s_client.SendMessage(om, NetDeliveryMethod.ReliableOrdered);
            Output("Sending '" + text + "'");
            s_client.FlushSendQueue();
        }
    }
}
