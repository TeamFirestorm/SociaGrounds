using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SociaGrounds.Model.Controllers;
using SociaGrounds.Model.GUI;

namespace SociaGrounds.Model.Screens
{
    public class SettingsScreen
    {
        public void Update()
        {
            SociaKeyBoard.CheckKeyState();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            DefaultBackground.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
