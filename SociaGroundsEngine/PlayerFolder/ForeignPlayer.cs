using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SociaGroundsEngine.World;

namespace SociaGroundsEngine.PlayerFolder
{
    public class ForeignPlayer : CPlayer
    {
        public ForeignPlayer(/*ContentManager Content,*/ Vector2 startPosition, Texture2D texture)
        {
            animation = new CAnimation(texture, startPosition, 64, 64, 10, 25, true);
            position = startPosition;
            speed = 3;

            rect = new Rectangle((int)position.X, (int)position.Y, 64, 64);
        }

        public override void update(GameTime gameTime, UI ui, Viewport viewPort, Map map)
        {
            animation.Position = position;
            rect = new Rectangle((int)position.X, (int)position.Y, 64, 64);
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            animation.draw(spriteBatch);
        }
    }
}
