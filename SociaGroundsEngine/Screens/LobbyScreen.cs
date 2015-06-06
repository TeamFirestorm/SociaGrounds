using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using SociaGroundsEngine.DataBase;
using SociaGroundsEngine.Multiplayer;

namespace SociaGroundsEngine.Screens
{
    public class LobbyScreen : Screen
    {
        private readonly List<Connection> connections;
        private PlayersSendHost Host { get; set; }
        private PlayersSendClient Client { get; set; }

        private bool createdList;

        public LobbyScreen()
        {
            InternetConnection.GetMyIpAndDns();
            createdList = false;
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
            if (createdList)
            {
                string ip = InternetConnection.CheckPossibleConnection(connections);

                if (ip == null)
                {
                    Host = new PlayersSendHost();
                }
                else
                {
                    Client = new PlayersSendClient(ip);
                }
                Game1.currentScreenState = Game1.ScreenState.RoomScreen;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}
