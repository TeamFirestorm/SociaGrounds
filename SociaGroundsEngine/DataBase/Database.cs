using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SociaGroundsEngine.DataBase
{
    public class Database
    {
        public static bool IsRunning { get; set; }

        public static async void InsertConnectionInfo(string ipaddress, string dnssuffix)
        {
            var request = (HttpWebRequest)WebRequest.Create("http://www.matthijsreeringh.nl/SociaGrounds/insertConnectionInfo.php?ipadress=" + ipaddress + "&dnssuffix=" + dnssuffix);
            
            await request.GetResponseAsync();

           // request.
        }

        public static bool DeleteConnection(string ipaddress, string dnssuffix)
        {
            var request = (HttpWebRequest)WebRequest.Create("http://www.matthijsreeringh.nl/SociaGrounds/deleteConnection.php?ipadress=" + ipaddress + "&dnssuffix=" + dnssuffix);
            //if (request.GetResponse().ToString() == "Succes")
            {
                return true;
            }
            return false;
        }

        public static List<Connection> GetConnections()
        {
            //string json = new WebClient().DownloadString("http://matthijsreeringh.nl/SociaGrounds/getConnections.php");

            //if (json != "")
            //{
            //    List<Connection> u = JsonConvert.DeserializeObject<List<Connection>>(json);

            //    return u;
            //}
            //else
            //{
                return null;
            //}
        }

        public async static Task<string> InsertUserAsync(string phoneID, string username)
        {
            IsRunning = true;

            HttpClient client = new HttpClient();

            Task<string> getStringTask = client.GetStringAsync("http://www.matthijsreeringh.nl/SociaGrounds/insertUser.php?phoneID=" + phoneID + "&username=" + username);

            string urlContents = await getStringTask;

            return urlContents;
        }
    }
}
