using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SociaGrounds.Model.Screens
{
    public class SettingsScreen
    {
        public void Update(KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.Escape))
            {
                Game1.CurrentScreenState = Game1.ScreenState.HomeScreen;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            DefaultBackground.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
