using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SociaGroundsEngine.GUI;
using SociaGroundsEngine.World;

namespace SociaGroundsEngine.PlayerFolder
{
    public class MyPlayer : CPlayer
    {
        public MyPlayer(/*ContentManager Content,*/ Vector2 startPosition, Texture2D texture)
        {
            animation = new CAnimation(texture, startPosition, 64, 64, 10, 25, true);
            position = startPosition;
            speed = 3;

            rect = new Rectangle((int)position.X, (int)position.Y, 64, 64);
        }

        public override void update(GameTime gameTime, Ui ui, Viewport viewPort, Map map)
        {
            input(gameTime, ui, viewPort, map);
            animation.Position = position;
            rect = new Rectangle((int)position.X, (int)position.Y, 64, 64);
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            animation.draw(spriteBatch);
        }

        // Method for all input
        public void input(GameTime gameTime, Ui ui, Viewport viewPort, Map map)
        {
            if (ui.touchUp(viewPort))
            {
                animation.play(8, 9, gameTime);

                if (!isCollidingTop(map))
                {
                    position.Y -= speed;
                }
            }
            else if(ui.touchDown(viewPort))
            {
                animation.play(10, 9, gameTime);

                if (!isCollidingBottom(map))
                {
                    position.Y += speed;
                }
            }
            else if(ui.touchLeft(viewPort))
            {
                animation.play(9, 9, gameTime);

                if (!isCollidingLeft(map))
                {
                    position.X -= speed; 
                }
            }
            else if (ui.touchRight(viewPort))
            {
                animation.play(11, 9, gameTime);

                if (!isCollidingRight(map))
                {
                    position.X += speed; 
                }
            }
        }

        // Check if the player has a collision on his right side
        public bool isCollidingRight(Map map)
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
        public bool isCollidingLeft(Map map)
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
        public bool isCollidingTop(Map map)
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
        public bool isCollidingBottom(Map map)
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
