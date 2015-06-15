using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SocialGroundsStore.GUI
{
    class SociaKeyboard
    {
        // OnPressedEnter event field
        public static event EnterPressed OnEnterPressed;
        public delegate void EnterPressed(string text);

        // Buttons
        private List<Button> buttons;
        public List<Button> Buttons
        {
            get { return buttons; }
        }

        // Position
        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        //// Height and width of the keyboard
        //private float height;
        //public float Height
        //{
        //    get { return height; }
        //}

        // Variables to check if the position has been updated
        private Vector2 oldPosition;
        private Vector2 newPosition;

        // Fields for building the string
        private string textBuffer;
        public string TextBuffer
        {
            get { return textBuffer; }
        }
        private readonly char[] chars = { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'z', 'x', 'c', 'v', 'b', 'n', 'm', ' ' };

        // The maximum amount of characters that can be typed
        private readonly int maxchars;

        public SociaKeyboard(ContentManager content, Vector2 position, int maxchars, float scale)
        {
            // Text initialize
            textBuffer = "";
            this.maxchars = maxchars;

            // Position initialize
            this.position = position;
            newPosition = position;
            oldPosition = position;
            CreateButtons(content, scale);
        }

        private void CreateButtons(ContentManager content, float scale)
        {
            // Button list initialize
            buttons = new List<Button>();
            int btHeight = 0;
            int width = 0;
            Vector2 beginRect = new Vector2(360, 400);

            for (int i = 0; i < chars.Length; i++)
            {
                if (width >= 10)
                {
                    btHeight++;
                    width = 0;
                    beginRect.X = 360;
                    beginRect.Y += 120;
                }
                buttons.Add(new Button(content, new Vector2(position.X + ((75 * scale) * width), position.Y + ((120 * scale) * btHeight)), chars[i].ToString(), scale, beginRect));
                beginRect.X += 75;
                width++;
            }

            // Backspace button
            buttons.Add(new Button(content, new Vector2(position.X + ((75 * scale) * width), position.Y + ((120 * scale) * btHeight)), "<----", scale, beginRect));

            width += 3;
            beginRect.X += 200;

            // Enter button
            buttons.Add(new Button(content, new Vector2(position.X + ((75 * scale) * width), position.Y + ((120 * scale) * btHeight)), "Enter", scale, beginRect));

        }

        /// <summary>
        /// Update logic for the keyboard
        /// Building the string if a button has been clicked for example
        /// </summary>
        public void update(Viewport viewport)
        {
            // Retrieve the latest position
            newPosition = position;

            // Update the button positions if the key board position has been updated
            float xDiff = oldPosition.X - newPosition.X;
            float yDiff = oldPosition.Y - newPosition.Y;
            foreach (Button button in buttons)
            {
                button.Position = new Vector2(button.Position.X - xDiff, button.Position.Y - yDiff);

                // Update all the button in the list while the loop is still there
                button.update(viewport,default(MouseState));
            }

            // Check all the buttons for a click
            int count = 0;
            foreach (Button button in buttons)
            {
                bool v = button.isTouched();

                // Backspace click
                if (v && count == buttons.Count - 2)
                {
                    // Build a new string without the last character
                    if (textBuffer.Length > 0)
                    {
                        char[] text = textBuffer.ToCharArray();
                        textBuffer = new string(text, 0, text.Length - 1);
                    }
                    break;
                }
                // Enter click
                else if (v && count == buttons.Count - 1)
                {
                    if (OnEnterPressed != null)
                    {
                        OnEnterPressed(textBuffer);
                        textBuffer = "";
                    }
                    break;
                }
                // Letter click
                else if (v && count < buttons.Count)
                {
                    // Check if the maximum amount of characters has been reached
                    if (textBuffer.Length < maxchars)
                    {
                        textBuffer += button.Text.ToString();
                    }
                    break;
                }

                count++;
            }

            // Save the old position
            oldPosition = newPosition;
        }

        /// <summary>
        /// Drawing the whole keyboard
        /// </summary>
        public void draw(SpriteBatch spriteBatch)
        {
            foreach (Button button in buttons)
            {
                button.draw(spriteBatch);
            }
        }
    }
}
