using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SociaGroundsEngine.DataBase
{
    public static class DbStuff
    {
        public static bool IsRunning { get; private set; }

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
            string ipaddress = InternetConnection.MyIp;
            string dnssuffix = InternetConnection.MyDnsSuffix;

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
            string ipaddress = InternetConnection.MyIp;
            string dnssuffix = InternetConnection.MyDnsSuffix;

            if (ipaddress == null || dnssuffix == null) return null;

            HttpClient client = new HttpClient();

            Task<string> getStringTask = client.GetStringAsync("http://www.matthijsreeringh.nl/SociaGrounds/deleteConnection.php?ipadress=" + ipaddress + "&dnssuffix=" + dnssuffix);

            string urlContents = await getStringTask;

            return urlContents;
        }
    }
}
