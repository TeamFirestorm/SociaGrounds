using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SociaGrounds.Model.Controllers;

namespace SociaGrounds.Model.Screens
{
    public class SettingsScreen
    {
        public void Update(KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.Escape))
            {
                Static.CurrentScreenState = ScreenState.HomeScreen;
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
