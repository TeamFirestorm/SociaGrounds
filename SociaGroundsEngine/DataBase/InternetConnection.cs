using System;
using System.Collections.Generic;
using Windows.Networking.Connectivity;

namespace SociaGroundsEngine.DataBase
{
    public static class InternetConnection
    {
        public static string MyIp { get; private set; }
        public static string MyDnsSuffix { get; private set; }

        public static void GetMyIpAndDns()
        {
            var hostnames = NetworkInformation.GetHostNames();
            foreach (var hn in hostnames)
            {
                //IanaInterfaceType == 71 => Wifi
                //IanaInterfaceType == 6 => Ethernet (Emulator)
                if (hn.IPInformation != null && (hn.IPInformation.NetworkAdapter.IanaInterfaceType == 71))
                {
                    MyIp = hn.DisplayName;
                    MyDnsSuffix = "";
                }
            }

            if (MyIp == null || MyDnsSuffix == null)
            {
                throw new Exception("No Wifi connection found!");
            }
        }

        public static string CheckPossibleConnection()
        {
            if (MyIp == null || MyDnsSuffix == null) return null;

            List<Connection> connections = DataBase.GetConnections();

            if (connections != null)
            {
                string[] tempIp = MyIp.Split('.');
                string myIp = tempIp[0] + "." + tempIp[1] + "." + tempIp[2];

                foreach (Connection connect in connections)
                {
                    tempIp = connect.IpAddress.Split('.');
                    string ip = tempIp[0] + "." + tempIp[1] + "." + tempIp[2];

                    if (ip.Equals(myIp) && connect.DnsSuffix.Equals(MyDnsSuffix))
                    {
                        return connect.IpAddress;
                    }
                }
            }
            return null;
        }
    }
}
