﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;


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
    }
}