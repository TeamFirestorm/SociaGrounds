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
    class RoomMenu
    {
        Button showHideButton;
        SociaKeyboard keyboard;

        // Boolean to check if the keyboard is up
        bool isKeyboardUp;
        public bool IsKeyboardUp
        {
            get { return isKeyboardUp; }
        }

        // Viewport that is saved within this class
        Viewport viewport;

        // Inputfield for the chat
        SociaInputfield inputField;

        public RoomMenu(ContentManager content, Viewport viewport)
        {
            this.viewport = viewport;
            isKeyboardUp = false;

            inputField = new SociaInputfield(content, new Vector2(viewport.Width / 4, viewport.Height / 1.15f), 10, 0.15f);
            showHideButton = new Button(content, new Vector2(viewport.Width / 100, viewport.Height / 1.15f), "show/hide ", 0.75f);
            keyboard = new SociaKeyboard(content, new Vector2(viewport.Width / 4, viewport.Height), 35);
        }

        public void update()
        {
            keyboard.update();

            // Check if the Show/Hide button is clicked
            if (showHideButton.isTouched() && !isKeyboardUp && keyboard.Position.Y >= viewport.Height)
            {
                isKeyboardUp = true;
            }
            else if (showHideButton.isTouched() && isKeyboardUp && keyboard.Position.Y <= viewport.Height / 2)
            {
                isKeyboardUp = false;
            }

            // Updating the keyboard position if the the Show/Hide button has been clicked
            // Keyboard up
            if (isKeyboardUp && keyboard.Position.Y > viewport.Height / 2)
            {
                keyboard.Position = new Vector2(keyboard.Position.X, keyboard.Position.Y - 30);
                inputField.Position = new Vector2(inputField.Position.X, inputField.Position.Y - 30);
            }
            // Keyboard down
            else if (!isKeyboardUp && keyboard.Position.Y < viewport.Height)
            {
                keyboard.Position = new Vector2(keyboard.Position.X, keyboard.Position.Y + 30);
                inputField.Position = new Vector2(inputField.Position.X, inputField.Position.Y + 30);
            }

            // Input field update
            inputField.update(keyboard.TextBuffer);
        }

        public void draw(SpriteBatch spriteBatch)
        {
            showHideButton.draw(spriteBatch);
            keyboard.draw(spriteBatch);
            inputField.draw(spriteBatch);
        }
    }
}
