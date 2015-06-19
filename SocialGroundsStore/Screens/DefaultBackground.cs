using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SocialGroundsStore.Screens
{
    public class DefaultBackground
    {
        private static Texture2D _background;
        private static Texture2D _title;

        public DefaultBackground(ContentManager content)
        {
            _background = content.Load<Texture2D>("Background/background.png");
            _title = content.Load<Texture2D>("Background/Sociagrounds_title.png");
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_background, new Vector2(0, 0), Color.White);

            spriteBatch.Draw(_title, new Vector2((Game1.Viewport.Width / 2f - _title.Width / 2f), 50), Color.White);
        }
    }
}
