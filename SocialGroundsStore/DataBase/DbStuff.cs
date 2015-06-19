using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;
using Newtonsoft.Json;

namespace SocialGroundsStore.DataBase
{
    public static class DbStuff
    {
        private static bool IsRunning { get; set; }
        private static string MyIp { get; set; }
        private static string MyDnsSuffix { get; set; }

        static DbStuff()
        {
            IsRunning = false;
        }

        public async static Task<string> InsertUser(string phoneId, string username)
        {
            IsRunning = true;

            HttpClient client = new HttpClient();

            Task<string> getStringTask = client.GetStringAsync("http://www.matthijsreeringh.nl/SociaGrounds/insertUser.php?phoneID=" + phoneId + "&username=" + username);

            string urlContents = await getStringTask;

            return urlContents;
        }

        public async static Task<List<Connection>> GetConnections()
        {
            HttpClient client = new HttpClient();

            Task<string> getStringTask = client.GetStringAsync("http://www.matthijsreeringh.nl/SociaGrounds/getConnections.php");

            string urlContents = await getStringTask;

            if (urlContents != "")
            {
                List<Connection> u = JsonConvert.DeserializeObject<List<Connection>>(urlContents);

                return u;
            }
            return null;
        }

        public async static Task<string> InsertConnection()
        {
            string ipaddress = MyIp;
            string dnssuffix = MyDnsSuffix;

            if (dnssuffix == "")
            {
                dnssuffix = "geen";
            }

            if (ipaddress == null || dnssuffix == null) return null;

            HttpClient client = new HttpClient();

            Task<string> getStringTask = client.GetStringAsync("http://www.matthijsreeringh.nl/SociaGrounds/insertConnectionInfo.php?ipadress=" + ipaddress + "&dnssuffix=" + dnssuffix);

            string urlContents = await getStringTask;

            return urlContents;
        }

        public static async Task<string> DeleteConnection()
        {
            string ipaddress = MyIp;
            string dnssuffix = MyDnsSuffix;

            if (ipaddress == null || dnssuffix == null) return null;

            HttpClient client = new HttpClient();

            Task<string> getStringTask = client.GetStringAsync("http://www.matthijsreeringh.nl/SociaGrounds/deleteConnection.php?ipadress=" + ipaddress + "&dnssuffix=" + dnssuffix);

            string urlContents = await getStringTask;

            return urlContents;
        }

        public static void GetMyIpAndDns()
        {
            var hostnames = NetworkInformation.GetHostNames();
            foreach (var hn in hostnames)
            {
                //IanaInterfaceType == 71 => Wifi
                //IanaInterfaceType == 6 => Ethernet (Emulator)
                if (hn.IPInformation != null)
                {
                    if (hn.IPInformation.NetworkAdapter.IanaInterfaceType == 71 || hn.IPInformation.NetworkAdapter.IanaInterfaceType == 6)
                    {
                        MyIp = hn.DisplayName;
                        MyDnsSuffix = "";
                    }
                }
            }

            if (MyIp == null)
            {
                throw new Exception("No Wifi connection found!");
            }
        }

        public static string CheckPossibleConnection(List<Connection> connections)
        {
            if (MyIp == null || MyDnsSuffix == null) return null;

            IsRunning = true;

            string myDns = MyDnsSuffix;

            if (myDns == "")
            {
                myDns = "geen";
            }

            if (connections != null)
            {
                string[] tempIp = MyIp.Split('.');
                string myIp = tempIp[0] + "." + tempIp[1] + "." + tempIp[2];

                foreach (Connection connect in connections)
                {
                    tempIp = connect.IpAddress.Split('.');
                    string ip = tempIp[0] + "." + tempIp[1] + "." + tempIp[2];

                    if (ip.Equals(myIp) && connect.DnsSuffix.Equals(myDns))
                    {
                        if (IsRunning)
                        {
                            if (connect.IpAddress.Equals(MyIp)) return null;
                        }

                        return connect.IpAddress;
                    }
                }
            }
            return null;
        }
    }
}
