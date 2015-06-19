using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SociaGroundsEngine.World
{
    public class GrassTile : Asset
    {
        Texture2D texture;
        Texture2D Texture
        {
            get { return texture; }
        }

        public GrassTile(Texture2D texture, Vector2 position) : base(position)
        {
            this.texture = texture;
            this.position = position;
            rect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            isSolid = false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rect, Color.White);
        }
    }
}
