using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SociaGrounds.Model.Controllers;
using SociaGrounds.Model.GUI;

namespace SociaGrounds.Model.Screens
{
    public class SettingsScreen
    {
        public void Update(GameTime gameTime)
        {
            SociaKeyBoard.CheckKeyState(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            DefaultBackground.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
