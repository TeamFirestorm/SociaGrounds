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
    public class LobbyScreen
    {
        private readonly List<Connection> _connections;
        private PlayersSendHost Host { get; set; }
        private PlayersSendClient Client { get; set; }

        private bool _createdList;
        private bool _alreadyStarted;

        public LobbyScreen()
        {
            DbStuff.GetMyIpAndDns();
            _createdList = false;
            _alreadyStarted = false;
            _connections = new List<Connection>();
        }

        public async void CreateConnections()
        {
            _connections.Clear();
            List<Connection> temp = await DbStuff.GetConnections();
            foreach (var con in temp)
            {
                _connections.Add(con);
            }
            _createdList = true;
        }

        public override void Update(ContentManager content)
        {
            if (_createdList && !_alreadyStarted)
            {
                _alreadyStarted = true;

                string ip = DbStuff.CheckPossibleConnection(_connections);

                if (ip == null)
                {
                    Host = new PlayersSendHost(content);
                    Task.Run(new Action(Host.Loop));
                    Debug.WriteLine("Created and started Host");
                    InsertConnection();
                }
                else
                {
                    Host = null;
                    Client = new PlayersSendClient(content, ip);
                    Debug.WriteLine("Created and started Client");
                    Task.Run(new Action(Client.Loop));
                }
                Game1.currentScreenState = Game1.ScreenState.RoomScreen;
            }
        }

        private static async void InsertConnection()
        {
            await DbStuff.InsertConnection();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}
