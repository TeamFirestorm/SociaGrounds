using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SociaGrounds.Model.GUI;
using SociaGrounds.Model.GUI.Controls;

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
