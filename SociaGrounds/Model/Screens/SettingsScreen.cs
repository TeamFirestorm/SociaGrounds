using Microsoft.Xna.Framework.Graphics;
using SociaGrounds.Model.Controllers;

namespace SociaGrounds.Model.Screens
{
    public class SettingsScreen
    {
        public void Update()
        {
            Static.Keyboard.CheckKeyState();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            DefaultBackground.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
