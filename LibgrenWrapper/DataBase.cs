using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;


namespace LibgrenWrapper
{
    public class DataBase
    {
        public static bool InsertConnectionInfo(string ipaddress, string dnssuffix)
        {
            var request = (HttpWebRequest)WebRequest.Create("http://www.matthijsreeringh.nl/SociaGrounds/insertConnectionInfo.php?ipadress="+ipaddress+"&dnssuffix="+dnssuffix);
            if (request.GetResponse().ToString() == "Succes")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool DeleteConnection(string ipaddress, string dnssuffix)
        {
            var request = (HttpWebRequest)WebRequest.Create("http://www.matthijsreeringh.nl/SociaGrounds/deleteConnection.php?ipadress=" + ipaddress + "&dnssuffix=" + dnssuffix);
            if (request.GetResponse().ToString() == "Succes")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<Connection> GetConnections()
        {
            string json = new WebClient().DownloadString("http://matthijsreeringh.nl/SociaGrounds/getConnections.php");

            if (json != "")
            {
                List<Connection> u = JsonConvert.DeserializeObject<List<Connection>>(json);

                return u;
            }
            else
            {
                return null;
            }
        }

        public static void insertUser(string phoneID, string username)
        {
            var request = (HttpWebRequest)WebRequest.Create("http://www.matthijsreeringh.nl/SociaGrounds/insertUser.php?phoneID=" + phoneID + "&username=" + username);
            if (request.GetResponse().ToString() == "Succes")
            {
                return;
            }
            else
            {
                return;
            }
        }

        public async static Task<string> insertUserAsync(string phoneID, string username)
        {
            HttpClient client = new HttpClient();

            Task<string> getStringTask = client.GetStringAsync("http://www.matthijsreeringh.nl/SociaGrounds/insertUser.php?phoneID=" + phoneID + "&username=" + username);

            string urlContents = await getStringTask;

            return urlContents;
        }

    }
}
