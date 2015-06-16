﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SocialGroundsStore.GUI;
using SocialGroundsStore.World;

namespace SocialGroundsStore.PlayerFolder
{
    public class MyPlayer : CPlayer
    {
        private Direction lastDirection = Direction.Down;

        public MyPlayer(Vector2 startPosition, Texture2D texture, SpriteFont font, int id)
        {
            animation = new CAnimation(texture, startPosition, 64, 64, 10, 25, true);
            position = startPosition;
            speed = 3;

            rect = new Rectangle((int)position.X, (int)position.Y, 64, 64);

            // Chat message initialize
            this.font = font;
            chatMessage = "";

            Id = id;
        }

        public MyPlayer(Vector2 startPosition, Texture2D texture, SpriteFont font)
        {
            animation = new CAnimation(texture, startPosition, 64, 64, 10, 25, true);
            position = startPosition;
            speed = 3;
            // Chat message initialize
            this.font = font;
            chatMessage = "";

            rect = new Rectangle((int)position.X, (int)position.Y, 64, 64);
        }

        public override void Update(GameTime gameTime, Ui ui, Viewport viewPort, Map map, KeyboardState keyState)
        {
            Input(gameTime, map, keyState);
            animation.Position = position;

            rect = new Rectangle((int)position.X, (int)position.Y, 64, 64);

            // Show the chat message for a certain amount of time
            if (chatMessage != "")
            {
                chatCounter += gameTime.ElapsedGameTime.Milliseconds;
            }

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

        public override void Draw(SpriteBatch spriteBatch)
        {
            animation.Draw(spriteBatch);
            spriteBatch.DrawString(font, chatMessage, new Vector2(position.X - (chatMessage.Length * 4) + 20, position.Y - 10), Color.White);
        }

        // Method for all input
        public void Input(GameTime gameTime, Map map, KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.Up))
            {
                lastDirection = Direction.Up;
                if (!IsCollidingTop(map))
                {
                    animation.Play(8, 9, gameTime);
                    position.Y -= speed;
                }
            }
            else if (keyState.IsKeyDown(Keys.Down))
            {
                lastDirection = Direction.Down;
                if (!IsCollidingBottom(map))
                {
                    animation.Play(10, 9, gameTime);
                    position.Y += speed;
                }
            }
            else if (keyState.IsKeyDown(Keys.Left))
            {
                lastDirection = Direction.Left;
                if (!IsCollidingLeft(map))
                {
                    animation.Play(9, 9, gameTime);
                    position.X -= speed; 
                }
            }
            else if (keyState.IsKeyDown(Keys.Right))
            {
                lastDirection = Direction.Right;
                if (!IsCollidingRight(map))
                {
                    animation.Play(11, 9, gameTime);
                    position.X += speed; 
                }
            }
            else
            {
                if (lastDirection == Direction.Up) animation.ResetAnimation(8,gameTime);
                if (lastDirection == Direction.Left) animation.ResetAnimation(9, gameTime);
                if (lastDirection == Direction.Down) animation.ResetAnimation(10, gameTime);
                if (lastDirection == Direction.Right) animation.ResetAnimation(11, gameTime);
            }
        }

        // Check if the player has a collision on his right side
        public bool IsCollidingRight(Map map)
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
        public bool IsCollidingLeft(Map map)
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
        public bool IsCollidingTop(Map map)
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
        public bool IsCollidingBottom(Map map)
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
