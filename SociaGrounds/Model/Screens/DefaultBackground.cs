﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SociaGrounds.Model.Screens
{
    public class DefaultBackground
    {
        public static Texture2D Background { get; set; }
        public static Texture2D Title { get; set; }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Background, new Vector2(0, 0), Color.White);

            spriteBatch.Draw(Title, new Vector2((Game1.Viewport.Width / 2f - Title.Width / 2f), 50), Color.White);
        }
    }
}
