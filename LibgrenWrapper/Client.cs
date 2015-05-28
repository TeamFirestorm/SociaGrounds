using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lidgren.Network;
namespace LibgrenWrapper
{
    public class Client
    {
        private static NetClient s_client;

        public static async void Setup()
        {
            NetPeerConfiguration config = new NetPeerConfiguration("chat");
            config.AutoFlushSendQueue = false;
            s_client = new NetClient(config);

            s_client.RegisterReceivedCallback(new SendOrPostCallback(GotMessage));

            s_client.Shutdown("Bye");
        }

        private static void Output(string text)
        {
            Console.WriteLine(text);
        }


    }
}
