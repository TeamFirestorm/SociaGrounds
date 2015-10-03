﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SociaGrounds.Model.Controllers;

namespace SociaGrounds.Model.Screens
{
    public class AboutScreen
    {
        private readonly string[] _names;

        public AboutScreen()
        {
            _names = new [] {"Gyllion van Elderen", "Wouter Kosse", "Thijs Reeringh", "Chris Vinkers" /*, "Alwin Masseling"*/};
        }

        public void Update()
        {
            Static.Keyboard.CheckKeyState();
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            DefaultBackground.Draw(spriteBatch);

            float height = 0;

            spriteBatch.DrawString(Fonts.NormalFont, "Press escape to return", new Vector2(100,100), Color.Black);

            foreach (string name in _names)
            {
                spriteBatch.DrawString(Fonts.NormalFont, name, new Vector2(Static.ScreenSize.Width / 2f, ((Static.ScreenSize.Height / 2f) + height)), Color.Black);
                height += 40;
            }

            spriteBatch.End();
        }
    }
}
