using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;

namespace LibgrenWrapper
{
    public class DataBase
    {

        public bool insertConnectionInfo(string ipaddress, string dnssuffix)
        {
            var request = (HttpWebRequest)WebRequest.Create("http://www.matthijsreeringh.nl/SociaGrounds/insertConnectionInfo.php?ipadress="+ipaddress+"&dnssuffix="+dnssuffix);
            request.GetResponse();
            
        }

    }
}
