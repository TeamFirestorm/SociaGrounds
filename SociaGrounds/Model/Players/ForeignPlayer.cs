using System.Collections.Generic;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SociaGrounds.Model.Players
{
    public class ForeignPlayer : Player
    {
        private Vector2 _newPosition;

        /// <summary>
        /// Constructor of the foreign player AKA the host.
        /// This version is used by everyone playing the game.
        /// </summary>
        /// <param name = "startPosition" > The startposition of the player when entering the room</param>
        /// <param name = "connection" > The connection received by the host to make a connection</param>
        /// <param name = "id" > The ID of the player relevant for the server</param>
        public ForeignPlayer(Vector2 startPosition, NetConnection connection, int id)
        {
            _NewQPosition = new Queue<Vector2>();

            _Animation = new Animation(StaticPlayer.PlayerTexture, startPosition, 64, 64, 10, 25, false);
            _Position = startPosition;
            _Speed = 3;
            Connection = connection;

            _Rect = new Rectangle((int)_Position.X, (int)_Position.Y, 64, 64);

            _ChatMessage = "";

            Id = id;

            _newPosition = default(Vector2);
        }

        /// <summary>
        /// Constructor for the players that are not the host but are clients.
        /// Because the host sends out the neccesary information it is not neccesary for the client
        /// to contain the connection details.
        /// </summary>
        /// <param name="startPosition">The startposition of the player when entering the room</param>
        /// <param name="id">The ID of the player relevant for the server</param>
        public ForeignPlayer(Vector2 startPosition, int id)
        {
            _NewQPosition = new Queue<Vector2>();

            _Animation = new Animation(StaticPlayer.PlayerTexture, startPosition, 64, 64, 10, 25, false);
            _Position = startPosition;
            _Speed = 3;

            _Rect = new Rectangle((int)_Position.X, (int)_Position.Y, 64, 64);

            _ChatMessage = "";

            Id = id;

            _newPosition = default(Vector2);
        }

        /// <summary>
        /// This method is used to receive the new _Position data from the host
        /// </summary>
        /// <param name="pos">Positions of the foreign players</param>
        public void AddNewPosition(Vector2 pos)
        {
            _NewQPosition.Enqueue(pos);
        }

        /// <summary>
        /// The update method for the foreign player
        /// </summary>
        /// <param name="gameTime">Gametime object</param>
        /// <param name="state"></param>
        public override void Update(GameTime gameTime, KeyboardState state = default(KeyboardState))
        {
            if (_NewQPosition.Count > 0)
            {
                NewPosition(gameTime);
            }

            base.Update(gameTime,state);
        }

        /// <summary>
        /// Sets the new _Position of a foreign player
        /// </summary>
        /// <param name="gameTime"></param>
        private void NewPosition(GameTime gameTime)
        {
            if (_newPosition == default(Vector2))
            {
                if (_NewQPosition.Count > 0)
                {
                    _newPosition = _NewQPosition.Dequeue();
                }
                else
                {
                    return;
                }
            }

            if (_newPosition.Y > _Position.Y)
            {
                _Animation.Play(10, 9, gameTime);

                _Position.Y += (_newPosition.Y - _Position.Y);
            }
            else if (_newPosition.Y < _Position.Y)
            {
                _Animation.Play(8, 9, gameTime);

                _Position.Y -= (_Position.Y - _newPosition.Y);
            } 
            else if (_newPosition.X < _Position.X)
            {
                _Animation.Play(9, 9, gameTime);

                _Position.X -= (_Position.X - _newPosition.X);
            }
            else if (_newPosition.X > _Position.X)
            {
                _Animation.Play(11, 9, gameTime);

                _Position.X += (_newPosition.X - _Position.X);
            }

            if (_Position == _newPosition)
            {
                _newPosition = default(Vector2);
            }
        }
        
        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="spriteBatch">The spritebatch used for drawing</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            _Animation.Draw(spriteBatch);
        }
    }
}
