using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SociaGrounds.Model.Controllers;
using SociaGrounds.Model.GUI;
using SociaGrounds.Model.World;

namespace SociaGrounds.Model.Players
{
    public class MyPlayer : CPlayer
    {
        private Direction _lastDirection = Direction.Down;

        /// <summary>
        /// Constructor of the player that can be controlled with direct input
        /// This version is used if the player is a client
        /// </summary>
        /// <param name="startPosition">The startposition of the player when entering the room</param>
        /// <param name="texture">The spritesheet of the player</param>
        /// <param name="id">The ID of the player relevant for the server</param>
        public MyPlayer(Vector2 startPosition, Texture2D texture, int id)
        {
            animation = new CAnimation(texture, startPosition, 64, 64, 10, 25, true);
            position = startPosition;
            speed = 3;

            rect = new Rectangle((int)position.X, (int)position.Y, 64, 64);

            // Chat message initialize
            font = Static.Font;
            chatMessage = "";

            Id = id;
        }

        /// <summary>
        /// Constructor of the player that can be controlled with direct input
        /// This version is used for the host
        /// </summary>
        /// <param name="startPosition">The startposition of the player when entering the room</param>
        /// <param name="texture">The spritesheet of the player</param>
        public MyPlayer(Vector2 startPosition, Texture2D texture)
        {
            // General initialize
            animation = new CAnimation(texture, startPosition, 64, 64, 10, 25, true);
            position = startPosition;
            speed = 3;

            // Chat message initialize
            font = Static.Font;
            chatMessage = "";

            // Rectangle initialize
            rect = new Rectangle((int)position.X, (int)position.Y, 64, 64);
        }

        /// <summary>
        /// The update method for the player
        /// </summary>
        /// <param name="gameTime">Gametime object</param>
        /// <param name="ui">The UI object relevant to the player</param>
        /// <param name="viewPort">For the height and width of the screen</param>
        /// <param name="map">All info about the map</param>
        /// <param name="keyState">The current keystate</param>
        public override void Update(GameTime gameTime, Map map, KeyboardState state = default(KeyboardState))
        {
            animation.Position = position;

            rect = new Rectangle((int)position.X, (int)position.Y, 64, 64);

            Input(gameTime, map, state);

            // Show the chat message for a certain amount of time
            if (!string.IsNullOrEmpty(chatMessage))
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
        /// Draw method for 
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            animation.Draw(spriteBatch);
        }

        /// <summary>
        /// Method for all input for the palyer
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="map"></param>
        /// <param name="keyState"></param>
        public void Input(GameTime gameTime, Map map, KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.Up))
            {
                _lastDirection = Direction.Up;

                if (IsCollidingTop(map)) return;

                animation.Play(8, 9, gameTime);
                position.Y -= speed;
            }
            else if (keyState.IsKeyDown(Keys.Down))
            {
                _lastDirection = Direction.Down;

                if (IsCollidingBottom(map)) return;

                animation.Play(10, 9, gameTime);
                position.Y += speed;
            }
            else if (keyState.IsKeyDown(Keys.Left))
            {
                _lastDirection = Direction.Left;

                if (IsCollidingLeft(map)) return;

                animation.Play(9, 9, gameTime);
                position.X -= speed;
            }
            else if (keyState.IsKeyDown(Keys.Right))
            {
                _lastDirection = Direction.Right;

                if (IsCollidingRight(map)) return;

                animation.Play(11, 9, gameTime);
                position.X += speed;
            }
            else
            {
                if (_lastDirection == Direction.Up) animation.ResetAnimation(8,gameTime);
                if (_lastDirection == Direction.Left) animation.ResetAnimation(9, gameTime);
                if (_lastDirection == Direction.Down) animation.ResetAnimation(10, gameTime);
                if (_lastDirection == Direction.Right) animation.ResetAnimation(11, gameTime);
            }
        }

        // Check if the player has a collision on his right side
        private bool IsCollidingRight(Map map)
        {
            // Check all the solid assets in the map
            foreach (Asset asset in map.SolidAssets)
            {
                // If a right collision has been found, return true
                if (rect.TouchLeftOf(asset.Rect))
                {
                    return true;
                }
            }

            // If no collision has been found, return false
            return false;
        }

        // Check if the player has a collision on his left side
        private bool IsCollidingLeft(Map map)
        {
            // Check all the solid assets in the map
            foreach (Asset asset in map.SolidAssets)
            {
                // If a right collision has been found, return true
                if (rect.TouchRightOf(asset.Rect))
                {
                    return true;
                }
            }

            // If no collision has been found, return false
            return false;
        }

        // Check if the player has a collision on his top side
        private bool IsCollidingTop(Map map)
        {

            // Check all the solid assets in the map
            foreach (Asset asset in map.SolidAssets)
            {
                // If a right collision has been found, return true
                if (rect.TouchBottomOf(asset.Rect))
                {
                    return true;
                }
            }

            // If no collision has been found, return false
            return false;
        }

        // Check if the player has a collision on his bottom side
        private bool IsCollidingBottom(Map map)
        {
            // Check all the solid assets in the map
            foreach (Asset asset in map.SolidAssets)
            {
                // If a right collision has been found, return true
                if (rect.TouchTopOf(asset.Rect))
                {
                    return true;
                }
            }

            // If no collision has been found, return false
            return false;
        }
    }
}
