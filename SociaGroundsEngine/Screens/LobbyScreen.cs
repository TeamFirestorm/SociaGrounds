using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using SociaGroundsEngine.DataBase;
using SociaGroundsEngine.Multiplayer;

namespace SociaGroundsEngine.Screens
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

        public override void Update()
        {
            if (createdList && !alreadyStarted)
            {
                alreadyStarted = true;

                string ip = InternetConnection.CheckPossibleConnection(connections);

                if (ip == null)
                {
                    Host = new PlayersSendHost();
                    Debug.WriteLine("Created Host and Client");
                }
                else
                {
                    Host = null;
                    Client = new PlayersSendClient(ip);
                    Debug.WriteLine("Client");
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
