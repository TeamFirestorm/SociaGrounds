﻿using System;
using System.Collections.Generic;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SociaGrounds.Model.Controllers;
using SociaGrounds.Model.GUI;
using SociaGrounds.Model.World;

namespace SociaGrounds.Model.Players
{
    public class ForeignPlayer : CPlayer
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
            NewQPosition = new Queue<Vector2>();

            Animation = new CAnimation(Static.PlayerTexture, startPosition, 64, 64, 10, 25, false);
            position = startPosition;
            Speed = 3;
            this.connection = connection;

            Rect = new Rectangle((int)position.X, (int)position.Y, 64, 64);

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
            NewQPosition = new Queue<Vector2>();

            Animation = new CAnimation(Static.PlayerTexture, startPosition, 64, 64, 10, 25, false);
            position = startPosition;
            Speed = 3;

            Rect = new Rectangle((int)position.X, (int)position.Y, 64, 64);

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
            NewQPosition.Enqueue(pos);
        }

        /// <summary>
        /// The update method for the foreign player
        /// </summary>
        /// <param name="gameTime">Gametime object</param>
        /// <param name="ui">The UI object relevant to the player</param>
        /// <param name="viewPort">For the height and width of the screen</param>
        /// <param name="map">All info about the map</param>
        /// <param name="keyState">The current keystate</param>
        public override void Update(GameTime gameTime, Map map, KeyboardState state = default(KeyboardState))
        {
            if (NewQPosition.Count <= 0) return;

            NewPosition(gameTime);

            Animation.Position = position;

            Rect = new Rectangle((int)position.X, (int)position.Y, 64, 64);

            // Show the chat message for a certain amount of time
            if (!string.IsNullOrEmpty(chatMessage))
            {
                ChatCounter += gameTime.ElapsedGameTime.Milliseconds;

                if (ChangedText)
                {
                    ChatCounter = 0;
                    ChangedText = false;
                }

                // Then flush the chat message and reset the counter
                if (ChatCounter >= 5000)
                {
                    chatMessage = "";
                    ChatCounter = 0;
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
                if (NewQPosition.Count > 0)
                {
                    _newPosition = NewQPosition.Dequeue();
                }
                else
                {
                    return;
                }
            }

            if (_newPosition.Y > position.Y)
            {
                Animation.Play(10, 9, gameTime);

                position.Y += (_newPosition.Y - position.Y);
            }
            else if (_newPosition.Y < position.Y)
            {
                Animation.Play(8, 9, gameTime);

                position.Y -= (position.Y - _newPosition.Y);
            } 
            else if (_newPosition.X < position.X)
            {
                Animation.Play(9, 9, gameTime);

                position.X -= (position.X - _newPosition.X);
            }
            else if (_newPosition.X > position.X)
            {
                Animation.Play(11, 9, gameTime);

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
            Animation.Draw(spriteBatch);
        }
    }
}
