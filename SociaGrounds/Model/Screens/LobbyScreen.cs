using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SociaGrounds.Model.Controllers;
using SociaGrounds.Model.DB;
using SociaGrounds.Model.Multiplayer;

namespace SociaGrounds.Model.Screens
{
    public class LobbyScreen
    {
        private readonly List<Connection> _connections;

        private PlayersSendHost Host { get; set; }
        private PlayersSendClient Client { get; set; }

        private bool _createdList;
        private bool _alreadyStarted;
        public bool FirstStarted { get; set; }

        public LobbyScreen()
        {
            DataBase.GetMyIpAndDns();
            _createdList = false;
            _alreadyStarted = false;
            FirstStarted = false;
            _connections = new List<Connection>();
        }

        public async void CreateConnections()
        {
            _connections.Clear();
            List<Connection> temp = await DataBase.GetConnections();
            foreach (var con in temp)
            {
                _connections.Add(con);
            }
            _createdList = true;
        }

        public void Update(ContentManager content)
        {
            if (_createdList && !_alreadyStarted)
            {
                _alreadyStarted = true;

                string ip = DataBase.CheckPossibleConnection(_connections);

                if (ip == null)
                {
                    Host = new PlayersSendHost(content);
                    Task.Run(new Action(Host.Loop));
                    InsertConnection();
                }
                else
                {
                    Host = null;
                    Client = new PlayersSendClient(content, ip);
                    Task.Run(new Action(Client.Loop));
                }
                Static.CurrentScreenState = ScreenState.RoomScreen;
            }
        }

        private static async void InsertConnection()
        {
            await DataBase.InsertConnection();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            DefaultBackground.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
