using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SocialGroundsStore.DataBase;
using SocialGroundsStore.Multiplayer;

namespace SocialGroundsStore.Screens
{
    public class LobbyScreen : Screen
    {
        private readonly List<Connection> connections;
        public PlayersSendHost Host { get; set; }
        private PlayersSendClient Client { get; set; }

        private bool createdList;
        private bool alreadyStarted;

        public LobbyScreen()
        {
            InternetConnection.GetMyIpAndDns();
            createdList = false;
            alreadyStarted = false;
            connections = new List<Connection>();
            CreateConnections();
        }

        private async void CreateConnections()
        {
            connections.Clear();
            List<Connection> temp = await DbStuff.GetConnections();
            foreach (var con in temp)
            {
                connections.Add(con);
            }
            createdList = true;
        }

        public override void Update(ContentManager content)
        {
            if (createdList && !alreadyStarted)
            {
                alreadyStarted = true;

                string ip = InternetConnection.CheckPossibleConnection(connections);

                if (ip == null)
                {
                    Host = new PlayersSendHost(content);
                    Task.Run(new Action(Host.StartLoop));

                    Debug.WriteLine("Created and started Host");
                }
                else
                {
                    Host = null;
                    Client = new PlayersSendClient(content, ip);
                    Debug.WriteLine("Created and started Client");
                }

                #pragma warning disable 4014
                DbStuff.InsertConnection();
                #pragma warning restore 4014

                Game1.currentScreenState = Game1.ScreenState.RoomScreen;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}
