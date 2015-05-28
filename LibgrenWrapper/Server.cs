using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Lidgren.Network;

namespace LibgrenWrapper
{
    public class Server
    {
        private static NetServer _sServer;
        private static DispatcherTimer _timer;

        private static IPAddress _myIp;
        private static string _myDnsSuffix;

        public static async void Setup()
        {
            // set up network
            NetPeerConfiguration config = new NetPeerConfiguration("chat")
            {
                MaximumConnections = 100,
                Port = 14242
            };

            _sServer = new NetServer(config);

            _timer = new DispatcherTimer {Interval = new TimeSpan(0, 0, 0, 1)};
            _timer.Tick += TimerOnTick;
            _timer.Start();

            GetMyIpAndDns();
        }

        private static void TimerOnTick(object sender, EventArgs eventArgs)
        {
            NetIncomingMessage im;
            while ((im = _sServer.ReadMessage()) != null)
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
                        List<NetConnection> all = _sServer.Connections; // get copy
                        all.Remove(im.SenderConnection);

                        if (all.Count > 0)
                        {
                            NetOutgoingMessage om = _sServer.CreateMessage();
                            om.Write(NetUtility.ToHexString(im.SenderConnection.RemoteUniqueIdentifier) + " said: " + chat);
                            _sServer.SendMessage(om, all, NetDeliveryMethod.ReliableOrdered, 0);
                        }
                        break;

                    default:
                        Output("Unhandled type: " + im.MessageType + " " + im.LengthBytes + " bytes " + im.DeliveryMethod + "|" + im.SequenceChannel);
                        break;
                }
                _sServer.Recycle(im);
            }
        }
        
        private static void Output(string text)
        {
            Console.WriteLine(text);
        }

        private static void UpdateConnectionsList()
        {

            foreach (NetConnection conn in _sServer.Connections)
            {
                string str = NetUtility.ToHexString(conn.RemoteUniqueIdentifier) + " from " + conn.RemoteEndPoint.ToString() + " [" + conn.Status + "]";
                Console.WriteLine(str + "\n");
            }
        }

        // called by the UI
        public static void StartServer()
        {
            _sServer.Start();
        }

        // called by the UI
        public static void Shutdown()
        {
            _sServer.Shutdown("Requested by user");
        }

        private static void GetMyIpAndDns()
        {
            // Get a list of all network interfaces (usually one per network card, dialup, and VPN connection) 
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            
            foreach (NetworkInterface network in networkInterfaces)
            {
                // Read the IP configuration for each network 
                IPInterfaceProperties properties = network.GetIPProperties();

                // Each network interface may have multiple IP addresses 
                foreach (UnicastIPAddressInformation address in properties.UnicastAddresses)
                {
                    if (address.Address.AddressFamily == AddressFamily.InterNetwork && properties.DnsSuffix != "")
                    {
                        _myIp = address.Address;
                        _myDnsSuffix = properties.DnsSuffix;
                        break;
                    }
                }
            }
            throw new Exception("Not Connected to the Interwebs");
        }
    }
}
