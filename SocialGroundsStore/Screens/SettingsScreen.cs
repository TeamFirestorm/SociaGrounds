using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SocialGroundsStore.Screens
{
    public class SettingsScreen
    {
        public SettingsScreen()
        {
            
        }

        public void Update(KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.Escape))
            {
                Game1.currentScreenState = Game1.ScreenState.HomeScreen;
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
