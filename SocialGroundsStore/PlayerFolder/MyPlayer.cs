using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SocialGroundsStore.GUI;
using SocialGroundsStore.World;

namespace SocialGroundsStore.PlayerFolder
{
    public class MyPlayer : CPlayer
    {
        public MyPlayer(Vector2 startPosition, Texture2D texture, int id)
        {
            animation = new CAnimation(texture, startPosition, 64, 64, 10, 25, true);
            position = startPosition;
            speed = 3;

            rect = new Rectangle((int)position.X, (int)position.Y, 64, 64);

            Id = id;
        }

        public MyPlayer(Vector2 startPosition, Texture2D texture)
        {
            animation = new CAnimation(texture, startPosition, 64, 64, 10, 25, true);
            position = startPosition;
            speed = 3;

            rect = new Rectangle((int)position.X, (int)position.Y, 64, 64);
        }

        public override void Update(GameTime gameTime, Ui ui, Viewport viewPort, Map map)
        {
            Input(gameTime, ui, viewPort, map);
            animation.Position = position;
            rect = new Rectangle((int)position.X, (int)position.Y, 64, 64);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            animation.Draw(spriteBatch);
        }

        // Method for all input
        public void Input(GameTime gameTime, Ui ui, Viewport viewPort, Map map)
        {
            KeyboardState newState = Keyboard.GetState();

            if (newState.IsKeyDown(Keys.Up))
            {
                animation.Play(8, 9, gameTime);

                if (!IsCollidingTop(map))
                {
                    position.Y -= speed;
                }
            }
            else if (newState.IsKeyDown(Keys.Down))
            {
                animation.Play(10, 9, gameTime);

                if (!IsCollidingBottom(map))
                {
                    position.Y += speed;
                }
            }
            else if (newState.IsKeyDown(Keys.Left))
            {
                animation.Play(9, 9, gameTime);

                if (!IsCollidingLeft(map))
                {
                    position.X -= speed; 
                }
            }
            else if (newState.IsKeyDown(Keys.Right))
            {
                animation.Play(11, 9, gameTime);

                if (!IsCollidingRight(map))
                {
                    position.X += speed; 
                }
            }
        }

        // Check if the player has a collision on his right side
        public bool IsCollidingRight(Map map)
        {
            // Check all the solid assets in the map
            foreach (Asset asset in map.SolidAssets)
            {
                // If a right collision has been found, return true
                if (RectangleHelper.TouchLeftOf(rect, asset.Rect))
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
                if (RectangleHelper.TouchRightOf(rect, asset.Rect))
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
                if (RectangleHelper.TouchBottomOf(rect, asset.Rect))
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
                if (RectangleHelper.TouchTopOf(rect, asset.Rect))
                {
                    return true;
                }
            }

            // If no collision has been found, return false
            return false;
        }
    }
}
