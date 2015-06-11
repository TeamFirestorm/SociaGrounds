using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SociaGroundsEngine.GUI
{
    class RoomMenu
    {
        Button showHideButton;
        SociaKeyboard keyboard;

        // Position of the room menu
        Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        // Boolean to check if the keyboard is up
        bool isKeyboardUp;
        public bool IsKeyboardUp
        {
            get { return isKeyboardUp; }
        }

        
        // Inputfield for the chat
        SociaInputfield inputField;

        // Variables for the height of the keyboard and the inputfield
        float keyboardHeight;
        float inputFieldHeight;

        public RoomMenu(ContentManager content, Vector2 position)
        {
            this.position = position;
            isKeyboardUp = false;

            // Initialize
            showHideButton = new Button(content, position, "show/hide ", 0.4f, new Vector2(80, 650));
            inputField = new SociaInputfield(content, new Vector2(position.X + 100, position.Y), 10, 0.08f);
            keyboard = new SociaKeyboard(content, new Vector2(position.X + 100, position.Y + 200), 35, 0.5f);

            // Keyboard and inputfield height
            keyboardHeight = 3;
            inputFieldHeight = 4.7f;
        }

        public void update(Vector2 newPosition, Viewport viewport)
        {
            bool isTouched = showHideButton.isTouched();

            // Check if the Show/Hide button is clicked
            // Toggle on
            if (isTouched && !isKeyboardUp && keyboardHeight <= 30)
            {
                isKeyboardUp = true;
            }
            // Toggle off
            else if (isTouched && isKeyboardUp && keyboardHeight >= 3)
            {
                isKeyboardUp = false;
            }
            

            // Updating the keyboard position if the the Show/Hide button has been clicked
            // Keyboard up
            if (isKeyboardUp && keyboardHeight <= 30)
            {
                keyboardHeight += 2;
            }
            // Keyboard down
            else if (!isKeyboardUp && keyboardHeight >= 3)
            {
                keyboardHeight -= 2;
            }

            // Updating the input field position if the Show/Hide button has been clicked
            // Input field up
            if (isKeyboardUp && inputFieldHeight <= 300f)
            {
                inputFieldHeight += 30;
            }
            // Input field down
            else if (!isKeyboardUp && inputFieldHeight >= 4.7f)
            {
                inputFieldHeight -= 30;
            }

            // Position updates
            showHideButton.Position = new Vector2(newPosition.X - (viewport.Width / 4.6f), newPosition.Y + (viewport.Height / 4.7f));
            keyboard.Position = new Vector2(newPosition.X - (viewport.Width / 11f), newPosition.Y + (viewport.Height / keyboardHeight));
            inputField.Position = new Vector2(newPosition.X - (viewport.Width / 11f), newPosition.Y + (viewport.Height / inputFieldHeight));

            // Input field update
            inputField.update(keyboard.TextBuffer);
            keyboard.update(viewport);
        }

        public void draw(SpriteBatch spriteBatch)
        {
            showHideButton.draw(spriteBatch);
            keyboard.draw(spriteBatch);
            inputField.draw(spriteBatch);
        }
    }
}
