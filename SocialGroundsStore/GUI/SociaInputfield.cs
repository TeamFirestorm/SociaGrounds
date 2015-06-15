using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SocialGroundsStore.GUI
{
    class SociaInputfield
    {
        // Textures for the inputfield
        Texture2D textFieldEnd;
        Texture2D textFieldMiddle;

        // Position of the inputfield
        Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        // String that will be updated in the textfield
        SpriteFont font;
        string text;
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        // Amount of middle pieces in the textfield
        int width;

        // Scale of the inputfield
        float scale;

        public SociaInputfield(ContentManager content, Vector2 position, int width, float scale)
        {
            // Loading the textures
            textFieldEnd = content.Load<Texture2D>("GUI/Inputfield/LeftTextField");
            textFieldMiddle = content.Load<Texture2D>("GUI/Inputfield/MiddleTextfield");

            // Initializing the position
            this.position = position;

            // Loading the text stuff
            font = content.Load<SpriteFont>("SociaGroundsFont");
            this.text = "";

            this.width = width;
            this.scale = scale;
        }

        public void update(string text)
        {
            // Update the text in the textfield
            this.text = text;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            // Left area draw
            spriteBatch.Draw(textFieldEnd, position, null, Color.White, 0f, new Vector2(0, 0), new Vector2(scale, scale), SpriteEffects.None, 0f);

            // Middle area draw
            for (int i = 0; i < width; i++)
            {
                spriteBatch.Draw(textFieldMiddle, new Vector2(position.X + (textFieldEnd.Width * scale) + ((textFieldMiddle.Width * scale) * i), position.Y), null, Color.White, 0f, new Vector2(0, 0), new Vector2(scale, scale), SpriteEffects.None, 0f);
            }

            // Right area draw
            spriteBatch.Draw(textFieldEnd, new Vector2(position.X + (textFieldEnd.Width * scale) + ((textFieldMiddle.Width * scale) * width), position.Y), null, Color.White, 0f, new Vector2(0, 0), new Vector2(scale, scale), SpriteEffects.None, 0f);

            // Text draw
            spriteBatch.DrawString(font, text, new Vector2(position.X + (30 * scale), position.Y + (80 * scale)), Color.White, 0f, new Vector2(0, 0), new Vector2(scale / 0.07f, scale / 0.07f), SpriteEffects.None, 0f);
        }
    }
}
