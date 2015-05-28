using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public override void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rect, Color.White);
        }
    }
}
