﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

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
    }
}
