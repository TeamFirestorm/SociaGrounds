﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace LibgrenWrapper
{
    public class Server
    {
        private static NetServer s_server;

        public async void Setup()
        {
            // set up network
            NetPeerConfiguration config = new NetPeerConfiguration("chat");
            config.MaximumConnections = 100;
            config.Port = 14242;
            s_server = new NetServer(config);
            
        }

        private static void Output(string text)
        {
            Console.WriteLine(text);
        }

        private static void UpdateConnectionsList()
        {

            foreach (NetConnection conn in s_server.Connections)
            {
                string str = NetUtility.ToHexString(conn.RemoteUniqueIdentifier) + " from " + conn.RemoteEndPoint.ToString() + " [" + conn.Status + "]";
                Console.WriteLine(str + "\n");
            }
        }

        // called by the UI
        public static void StartServer()
        {
            s_server.Start();
        }

        // called by the UI
        public static void Shutdown()
        {
            s_server.Shutdown("Requested by user");
        }


    }
}
