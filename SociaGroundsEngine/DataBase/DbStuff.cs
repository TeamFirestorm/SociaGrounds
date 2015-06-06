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

        public async static Task<string> InsertUserAsync(string phoneID, string username)
        {
            IsRunning = true;

            HttpClient client = new HttpClient();

            Task<string> getStringTask = client.GetStringAsync("http://www.matthijsreeringh.nl/SociaGrounds/insertUser.php?phoneID=" + phoneID + "&username=" + username);

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
    }
}
