using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SociaGroundsEngine.GUI
{
    class Button
    {
        Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        // Scaling properties
        float scale;
        int width;
        public int Width
        {
            get { return width; }
        }


        // Neutral textures
        Texture2D left;
        Texture2D mid;
        Texture2D right;

        // Clicked textures
        Texture2D leftClicked;
        Texture2D midClicked;
        Texture2D rightClicked;

        // Text stuff
        SpriteFont font;
        string text;

        // Rectangle for detection
        Rectangle rect;
        public Rectangle Rect
        {
            get { return rect; }
        }

        public Button(ContentManager content, Vector2 position, string text, float scale)
        {
            // Neutral textures initialize
            left = content.Load<Texture2D>("GUI/Button/StandardButtonLeft");
            mid = content.Load<Texture2D>("GUI/Button/StandardButtonMiddle");
            right = content.Load<Texture2D>("GUI/Button/StandardButtonRight");

            // Clicked textures initialize
            leftClicked = content.Load<Texture2D>("GUI/Button/PressedButtonLeft");
            midClicked = content.Load<Texture2D>("GUI/Button/PressedButtonMiddle");
            rightClicked = content.Load<Texture2D>("GUI/Button/PressedButtonRight");

            // Text stuff
            this.text = text;
            font = content.Load<SpriteFont>("SociaGroundsFont");

            // Other stuff
            this.position = position;
            width = text.Length / 5;

            rect = new Rectangle((int)position.X, (int)position.Y, (int)((left.Width * scale) + ((mid.Width * width) * scale) + (right.Width * scale)), (int)(mid.Height * scale));
        }


        // Method to check if the button is touched right now
        // Can be used for holding the button
        public bool isHold()
        {
            // Loop through all the locations where touch is possible
            foreach (TouchLocation touch in TouchPanel.GetState())
            {
                // Check if the position is touched within the button
                if (touch.Position.X >= rect.Left &&
                    touch.Position.X <= rect.Right &&
                    touch.Position.Y >= rect.Top &&
                    touch.Position.Y <= rect.Bottom)
                {
                    return true;
                }
            }

            // If no touch
            return false;
        }


        // Trigger if the button is released
        // Use this method as the event trigger
        public bool isTouched()
        {
            // Loop through all the locations where touch is possible
            foreach (TouchLocation touch in TouchPanel.GetState())
            {
                // Check if the position is releeased within the button
                if (touch.Position.X >= rect.Left &&
                    touch.Position.X <= rect.Right &&
                    touch.Position.Y >= rect.Top &&
                    touch.Position.Y <= rect.Bottom &&
                    touch.State == TouchLocationState.Released)
                {
                    return true;
                }
            }

            // If no release
            return false;
        }


        public void draw(SpriteBatch spriteBatch)
        {
            // If the button is clicked, draw the clicked button
            if (isHold())
            {
                // Drawing the left part
                spriteBatch.Draw(leftClicked, position, null, Color.White, 0f, new Vector2(0, 0), new Vector2(scale, scale), SpriteEffects.None, 0f);

                // Drawing the mid part
                for (int i = 0; i < width; i++)
                {
                    spriteBatch.Draw(midClicked, new Vector2(position.X + (left.Width * scale) + ((mid.Width * scale) * i), position.Y), null, Color.White, 0f, new Vector2(0, 0), new Vector2(scale, scale), SpriteEffects.None, 0f);
                }

                // Drawing the right part
                spriteBatch.Draw(rightClicked, new Vector2(position.X + (left.Width * scale) + ((mid.Width * scale) * width), position.Y), null, Color.White, 0f, new Vector2(0, 0), new Vector2(scale, scale), SpriteEffects.None, 0f);

                // Drawing the text
                spriteBatch.DrawString(font, text, new Vector2(position.X + (30 * scale), position.Y + (40 * scale)), Color.White, 0f, new Vector2(0, 0), new Vector2(scale / 0.5f, scale / 0.5f), SpriteEffects.None, 0f);
            }
            // If not, draw the neutral button
            else
            {
                // Drawing the left part
                spriteBatch.Draw(left, position, null, Color.White, 0f, new Vector2(0, 0), new Vector2(scale, scale), SpriteEffects.None, 0f);

                // Drawing the mid part
                for (int i = 0; i < width; i++)
                {
                    spriteBatch.Draw(mid, new Vector2(position.X + (left.Width * scale) + ((mid.Width * scale) * i), position.Y), null, Color.White, 0f, new Vector2(0, 0), new Vector2(scale, scale), SpriteEffects.None, 0f);
                }

                // Drawing the right part
                spriteBatch.Draw(right, new Vector2(position.X + (left.Width * scale) + ((mid.Width * scale) * width), position.Y), null, Color.White, 0f, new Vector2(0, 0), new Vector2(scale, scale), SpriteEffects.None, 0f);

                // Drawing the text
                spriteBatch.DrawString(font, text, new Vector2(position.X + (30 * scale), position.Y + (40 * scale)), Color.Black, 0f, new Vector2(0, 0), new Vector2(scale / 0.5f, scale / 0.5f), SpriteEffects.None, 0f);
            }
        }
    }
}
