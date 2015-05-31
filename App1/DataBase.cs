using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    public class Database
    {
        public static async void InsertConnectionInfo(string ipaddress, string dnssuffix)
        {
            var request = (HttpWebRequest)WebRequest.Create("http://www.matthijsreeringh.nl/SociaGrounds/insertConnectionInfo.php?ipadress=" + ipaddress + "&dnssuffix=" + dnssuffix);

            await request.GetResponseAsync();
            bool val = request.HaveResponse;
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

        public async static void GetConnections()
        {
            HttpClient http = new System.Net.Http.HttpClient();
            HttpResponseMessage response = await http.GetAsync("http://matthijsreeringh.nl/SociaGrounds/getConnections.php");
            string webresponse = await response.Content.ReadAsStringAsync();

            //string json = new WebClient().DownloadString("http://matthijsreeringh.nl/SociaGrounds/getConnections.php");

            if (webresponse != "")
            {
                //List<Connection> u = JsonConvert.DeserializeObject<List<Connection>>(webresponse);
            }

            //    return u;
            //}
            //else
            //{
            //    return null;
            //}
        }
    }
}

