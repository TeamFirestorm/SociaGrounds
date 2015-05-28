using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace LibgrenWrapper
{
    public static class InternetConnection
    {
        public static IPAddress MyIp { get; set; }
        public static string MyDnsSuffix { get; set; }

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
                    if (address.Address.AddressFamily == AddressFamily.InterNetwork && properties.DnsSuffix != "")
                    {
                        MyIp = address.Address;
                        MyDnsSuffix = properties.DnsSuffix;
                        break;
                    }
                }
            }

            if (MyIp.Equals(default(IPAddress)) || MyDnsSuffix.Equals(default(string)))
            {
                throw new Exception("Not Connected to the Interwebs");
            }
        }

        public static void din()
        {
            DataBase.
        }
    }
}
