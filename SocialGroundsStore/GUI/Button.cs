﻿using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace SocialGroundsStore.GUI
{
    class Button
    {
        // Position of the button
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
        public string Text
        {
            get { return text; }
        }

        // Rectangle for detection
        Rectangle rect;
        public Rectangle Rect
        {
            get { return rect; }
            set { rect = value; }
        }

        // The touch state
        TouchCollection currentState;
        TouchCollection oldState;

        // Touch position offset
        Vector2 offset;

        // First constructor
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
            this.width = text.Length / 5;
            this.scale = scale;
            this.offset = new Vector2(0, 0);

            rect = new Rectangle((int)(position.X * scale), (int)(position.Y * scale), (int)((left.Width * scale) + ((mid.Width * width) * scale) + (right.Width * scale)), (int)(mid.Height * scale));
        }

        public Button(ContentManager content, Vector2 position, string text, float scale, Vector2 rectangle)
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
            this.width = text.Length / 5;
            this.scale = scale;
            this.offset = new Vector2(0, 0);

            rect = new Rectangle((int)rectangle.X, (int)rectangle.Y, (int)(((width * mid.Width) + left.Width + right.Width) * scale), (int)(left.Height * scale));
        }

        public void Update(Viewport viewport, MouseState mouseState)
        {
            IsClicked(mouseState);
        }

        // Trigger if the button is released
        // Use this method as the event trigger
        public bool IsClicked(MouseState mouseState)
        {
            // Check if the position is pressed within the button
            if (mouseState.Position.X >= rect.Left &&
                mouseState.Position.X <= rect.Right &&
                mouseState.Position.Y >= rect.Top &&
                mouseState.Position.Y <= rect.Bottom)
            {
                return true;
            }
            return false;
        }

        // Return the pressure
        public void TestPressure()
        {
            Debug.WriteLine(oldState.Count);

            // Loop through all the locations where touch is possible
            //if (TouchPanel.GetState().Count > 0)
            //{
            //    Debug.WriteLine(TouchPanel.GetState().First().State);
            //}
            
            foreach (TouchLocation touch in TouchPanel.GetState())
            {
                Debug.WriteLine(touch.State);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // If the button is clicked, draw the clicked button
            if (false)
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
