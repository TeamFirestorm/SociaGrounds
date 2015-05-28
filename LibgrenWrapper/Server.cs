using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Lidgren.Network;

namespace LibgrenWrapper
{
    public class Server
    {
        private static NetServer s_server;
        private static DispatcherTimer timer;

        public static async void Setup()
        {
            // set up network
            NetPeerConfiguration config = new NetPeerConfiguration("chat");
            config.MaximumConnections = 100;
            config.Port = 14242;
            s_server = new NetServer(config);

            timer = new DispatcherTimer {Interval = new TimeSpan(0, 0, 0, 5)};
            timer.Tick += TimerOnTick;
            timer.Start();
        }

        private static void TimerOnTick(object sender, EventArgs eventArgs)
        {
                NetIncomingMessage im;
                while ((im = s_server.ReadMessage()) != null)
                {
                    if (im.LengthBits == 0)
                    {
                        return;
                    }

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

                            string reason = im.ReadString();
                            Output(NetUtility.ToHexString(im.SenderConnection.RemoteUniqueIdentifier) + " " + status + ": " + reason);

                            if (status == NetConnectionStatus.Connected)
                                Output("Remote hail: " + im.SenderConnection.RemoteHailMessage.ReadString());

                            UpdateConnectionsList();
                            break;

                        case NetIncomingMessageType.Data:
                            // incoming chat message from a client
                            string chat = im.ReadString();

                            Output("Broadcasting '" + chat + "'");

                            // broadcast this to all connections, except sender
                            List<NetConnection> all = s_server.Connections; // get copy
                            all.Remove(im.SenderConnection);

                            if (all.Count > 0)
                            {
                                NetOutgoingMessage om = s_server.CreateMessage();
                                om.Write(NetUtility.ToHexString(im.SenderConnection.RemoteUniqueIdentifier) + " said: " + chat);
                                s_server.SendMessage(om, all, NetDeliveryMethod.ReliableOrdered, 0);
                            }
                            break;

                        default:
                            Output("Unhandled type: " + im.MessageType + " " + im.LengthBytes + " bytes " + im.DeliveryMethod + "|" + im.SequenceChannel);
                            break;
                    }
                    s_server.Recycle(im);
                }
        }
        
        private static void Output(string text)
        {
            Console.WriteLine(text);
        }

        private static void UpdateConnectionsList()
        {

            foreach (NetConnection conn in s_server.Connections)
            {
                string str = NetUtility.ToHexString(conn.RemoteUniqueIdentifier) + " from " + conn.RemoteEndPoint.ToString() + " [" + conn.Status + "]";
                Console.WriteLine(str + "\n");
            }
        }

        // called by the UI
        public static void StartServer()
        {
            s_server.Start();
        }

        // called by the UI
        public static void Shutdown()
        {
            s_server.Shutdown("Requested by user");
        }


    }
}
