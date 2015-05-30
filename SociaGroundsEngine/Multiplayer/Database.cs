using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Xna.Framework.Input;

namespace SociaGroundsEngine.Multiplayer
{
    public class Database
    {
        public static async void InsertConnectionInfo(string ipaddress, string dnssuffix)
        {
            var request = (HttpWebRequest)WebRequest.Create("http://www.matthijsreeringh.nl/SociaGrounds/insertConnectionInfo.php?ipadress=" + ipaddress + "&dnssuffix=" + dnssuffix);
            
            await request.GetResponseAsync();
            bool val = request.HaveResponse;
            if (val)
            {
                await MessageBox.Show("title", "Wil je dit " + val.ToString(), new List<string> { "button1", "button2" });
            }
            else
            {
                
            }
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
