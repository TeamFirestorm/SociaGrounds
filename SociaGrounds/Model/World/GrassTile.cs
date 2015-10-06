using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SociaGrounds.Model.World
{
    public class GrassTile : Asset
    {
        private readonly Texture2D _texture;

        public GrassTile(Texture2D texture, Vector2 position) : base(position)
        {
            _texture = texture;
            _Rect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _Rect, Color.White);
        }
    }
}
