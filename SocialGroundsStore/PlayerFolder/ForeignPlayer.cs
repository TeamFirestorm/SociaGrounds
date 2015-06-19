using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SocialGroundsStore.GUI;
using SocialGroundsStore.Multiplayer.Lidgren.Network;
using SocialGroundsStore.World;

namespace SocialGroundsStore.PlayerFolder
{
    public class ForeignPlayer : CPlayer
    {
        private Vector2 _newPosition;

        /// <summary>
        /// Constructor of the foreign player AKA the host.
        /// This version is used by everyone playing the game.
        /// </summary>
        /// <param name="startPosition">The startposition of the player when entering the room</param>
        /// <param name="connection">The connection received by the host to make a connection</param>
        /// <param name="id">The ID of the player relevant for the server</param>
        public ForeignPlayer(Vector2 startPosition, NetConnection connection, int id)
        {
            newQPosition = new Queue<Vector2>();

            animation = new CAnimation(Game1.texture, startPosition, 64, 64, 10, 25, false);
            position = startPosition;
            speed = 3;
            this.connection = connection;

            rect = new Rectangle((int)position.X, (int)position.Y, 64, 64);

            font = Game1.font;
            chatMessage = "";

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
            newQPosition = new Queue<Vector2>();

            animation = new CAnimation(Game1.texture, startPosition, 64, 64, 10, 25, false);
            position = startPosition;
            speed = 3;

            rect = new Rectangle((int)position.X, (int)position.Y, 64, 64);

            font = Game1.font;
            chatMessage = "";

            Id = id;

            _newPosition = default(Vector2);
        }

        /// <summary>
        /// This method is used to receive the new position data from the host
        /// </summary>
        /// <param name="pos">Positions of the foreign players</param>
        public void AddNewPosition(Vector2 pos)
        {
            newQPosition.Enqueue(pos);
        }

        /// <summary>
        /// The update method for the foreign player
        /// </summary>
        /// <param name="gameTime">Gametime object</param>
        /// <param name="ui">The UI object relevant to the player</param>
        /// <param name="viewPort">For the height and width of the screen</param>
        /// <param name="map">All info about the map</param>
        /// <param name="keyState">The current keystate</param>
        public override void Update(GameTime gameTime, Gui ui, Viewport viewPort, Map map, KeyboardState keyState)
        {
            if (newQPosition.Count <= 0) return;

            NewPosition(gameTime);

            animation.Position = position;

            rect = new Rectangle((int)position.X, (int)position.Y, 64, 64);

            // Show the chat message for a certain amount of time
            if (!String.IsNullOrEmpty(chatMessage))
            {
                chatCounter += gameTime.ElapsedGameTime.Milliseconds;

                if (changedText)
                {
                    chatCounter = 0;
                    changedText = false;
                }

                // Then flush the chat message and reset the counter
                if (chatCounter >= 5000)
                {
                    chatMessage = "";
                    chatCounter = 0;
                }
            }
        }

        /// <summary>
        /// Sets the new position of a foreign player
        /// </summary>
        /// <param name="gameTime"></param>
        private void NewPosition(GameTime gameTime)
        {
            if (_newPosition == default(Vector2))
            {
                if (newQPosition.Count > 0)
                {
                    _newPosition = newQPosition.Dequeue();
                }
                else
                {
                    return;
                }
            }

            if (_newPosition.Y > position.Y)
            {
                animation.Play(10, 9, gameTime);

                position.Y += (_newPosition.Y - position.Y);
            }
            else if (_newPosition.Y < position.Y)
            {
                animation.Play(8, 9, gameTime);

                position.Y -= (position.Y - _newPosition.Y);
            } 
            else if (_newPosition.X < position.X)
            {
                animation.Play(9, 9, gameTime);

                position.X -= (position.X - _newPosition.X);
            }
            else if (_newPosition.X > position.X)
            {
                animation.Play(11, 9, gameTime);

                position.X += (_newPosition.X - position.X);
            }

            if (position == _newPosition)
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
            animation.Draw(spriteBatch);
        }
    }
}
