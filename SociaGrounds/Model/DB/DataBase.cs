using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;
using Newtonsoft.Json;

namespace SociaGrounds.Model.DB
{
    /// <summary>
    /// This class handles all the in and output to the database.
    /// </summary>
    public static class DataBase
    {
        private static bool IsRunning { get; set; }
        private static string MyIp { get; set; }
        private static string MyDnsSuffix { get; set; }

        static DataBase()
        {
            IsRunning = false;
        }

        /// <summary>
        /// Method that inserts a user into the database
        /// </summary>
        /// <param name="phoneId">The windows Phone ID</param>
        /// <param name="username">The chosen Username</param>
        /// <returns></returns>
        public async static Task<string> InsertUser(string phoneId, string username)
        {
            IsRunning = true;

            HttpClient client = new HttpClient();

            Task<string> getStringTask = client.GetStringAsync("http://www.matthijsreeringh.nl/SociaGrounds/insertUser.php?phoneID=" + phoneId + "&username=" + username);

            string urlContents = await getStringTask;

            return urlContents;
        }

        /// <summary>
        /// Method that returns all the available connections
        /// </summary>
        /// <returns>A list of avilable connections</returns>
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

        /// <summary>
        /// Method that inserts the connection info of the host into the database
        /// </summary>
        /// <returns>Wether or not the connection has been inserted</returns>
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

        /// <summary>
        /// Method that deletes the current connection out of the database when the host quits the game
        /// </summary>
        /// <returns>Wether or not the connection has been deleted</returns>
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

        /// <summary>
        /// This method records your ip adress and dns suffix needed for the hosting of the room
        /// IanaInterfaceType == 71 => Wifi
        /// IanaInterfaceType == 6 => Ethernet (Emulator)
        /// </summary>
        public static void GetMyIpAndDns()
        {
            var hostnames = NetworkInformation.GetHostNames();
            foreach (var hn in hostnames)
            {             
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

        /// <summary>
        /// This method tries to find a host on the network the player is connected to
        /// </summary>
        /// <param name="connections">A list of currently active connections</param>
        /// <returns>A ip that the player can connect to</returns>
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
