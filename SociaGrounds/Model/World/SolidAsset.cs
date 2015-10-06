using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SociaGrounds.Model.World
{
    public abstract class SolidAsset : Asset
    {
        public abstract void DrawShade(SpriteBatch spriteBatch);

        protected SolidAsset(Vector2 position) : base(position)
        {

        }
    }
}
