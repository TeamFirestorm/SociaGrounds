using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace LibgrenWrapper
{
    public static class InternetConnection
    {
        public static IPAddress MyIp { get; private set; }
        public static string MyDnsSuffix { get; private set; }

        public static void GetMyIpAndDns()
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
                    if (address.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        if (properties.DnsSuffix != "" || address.Address.ToString().Contains("192."))
                        {
                            MyIp = address.Address;
                            MyDnsSuffix = properties.DnsSuffix;
                            break;
                        }
                    }
                }
            }

            if (MyIp == null || MyDnsSuffix == null)
            {
                throw new Exception("Not Connected to the Interwebs");
            }
        }

        public static IPAddress CheckPossibleConnection()
        {
            List<Connection> connections = DataBase.GetConnections();

            if (connections != null)
            {
                string[] tempIp = MyIp.ToString().Split('.');
                string myIp = tempIp[0] + "." + tempIp[1] + "." + tempIp[2];

                foreach (Connection connect in connections)
                {
                    tempIp = connect.IPAddress.Split('.');
                    string ip = tempIp[0] + "." + tempIp[1] + "." + tempIp[2];

                    if (ip.Equals(myIp) && connect.DNSSuffix.Equals(MyDnsSuffix))
                    {
                        return IPAddress.Parse(connect.IPAddress);
                    }
                }
            }
            return null;
        }
    }
}
