using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SociaGroundsEngine.Screens
{
    public abstract class Screen
    {
        // Method to update the screen frequently
        public abstract void Update(ContentManager content);

        // Method to draw the screen frequently
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
