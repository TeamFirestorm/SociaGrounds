using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using SociaGroundsEngine.DataBase;
using SociaGroundsEngine.Multiplayer;

namespace SociaGroundsEngine.Screens
{
    public class LobbyScreen : Screen
    {
        private readonly List<Connection> connections;

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
                    PlayersSendHost host = new PlayersSendHost();
                }
                else
                {
                    PlayersSendClient client = new PlayersSendClient(ip);
                }
                Game1.currentScreenState = Game1.ScreenState.RoomScreen;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}
