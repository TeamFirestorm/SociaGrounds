using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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
