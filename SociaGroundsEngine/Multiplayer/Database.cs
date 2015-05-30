using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SociaGroundsEngine.Multiplayer
{
    public class Database
    {
        public static bool InsertConnectionInfo(string ipaddress, string dnssuffix)
        {
            var request = (HttpWebRequest)WebRequest.Create("http://www.matthijsreeringh.nl/SociaGrounds/insertConnectionInfo.php?ipadress=" + ipaddress + "&dnssuffix=" + dnssuffix);
            
            if (request.HaveResponse)
            {
                
            }
            

            //if (request.GetResponse().ToString() == "Succes")
            {
                return true;
            }
            return false;
            
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
    }
}
