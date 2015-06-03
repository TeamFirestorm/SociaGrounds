using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SociaGroundsEngine.GUI
{
    class SociaKeyboard
    {
        // OnPressedEnter event field
        public static event EnterPressed OnEnterPressed;
        public delegate void EnterPressed(string text);

        // Buttons
        List<Button> buttons;
        public List<Button> Buttons
        {
            get { return buttons; }
        }

        // Position
        Vector2 position;
        public Vector2 Position { get; set; }

        // Decide whether the keyboard is up or not
        bool isUp = false;

        // Fields for building the string
        string textBuffer;
        public string TextBuffer
        {
            get { return textBuffer; }
        }
        char[] chars;

        public SociaKeyboard(ContentManager content, Vector2 position)
        {
            // Text initialize
            chars = new[] { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'z', 'x', 'c', 'v', 'b', 'n', 'm', ' ' };
            textBuffer = "";

            // Position initialize
            this.position = position;

            // Button list initialize
            buttons = new List<Button>();
            int height = 0;
            int width = 0;
            for (int i = 0; i < chars.Length; i++)
            {
                if (width >= 10)
                {
                    height++;
                    width = 0;
                }

                buttons.Add(new Button(content, new Vector2(position.X + (75 * width), position.Y + (120 * height)), chars[i].ToString(), 1.0f));
                width++;
            }

            // Backspace button
            buttons.Add(new Button(content, new Vector2(position.X + (75 * width), position.Y + (120 * height)), "<----", 1.0f));

            width += 3;

            // Enter button
            buttons.Add(new Button(content, new Vector2(position.X + (75 * width), position.Y + (120 * height)), "Enter", 1.0f));
        }

        /// <summary>
        /// Update logic for the keyboard
        /// Building the string if a button has been clicked for example
        /// </summary>
        public void update()
        {
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
                    textBuffer += button.Text.ToString();
                    break;
                }

                count++;
            }
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
