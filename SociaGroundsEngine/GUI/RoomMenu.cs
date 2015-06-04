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

        public RoomMenu(ContentManager content, Vector2 position)
        {
            this.position = position;
            isKeyboardUp = false;

            showHideButton = new Button(content, position, "show/hide ", 0.75f);
            inputField = new SociaInputfield(content, new Vector2(position.X + 100, position.Y), 10, 0.05f);
            keyboard = new SociaKeyboard(content, new Vector2(position.X + 100, position.Y + 100), 35, 0.5f);
        }

        public void update()
        {
            keyboard.update();

            // Check if the Show/Hide button is clicked
            if (showHideButton.isTouched() && !isKeyboardUp && keyboard.Position.Y >= position.Y + 100)
            {
                isKeyboardUp = true;
            }
            else if (showHideButton.isTouched() && isKeyboardUp && keyboard.Position.Y <= position.Y - 100)
            {
                isKeyboardUp = false;
            }

            // Updating the keyboard position if the the Show/Hide button has been clicked
            // Keyboard up
            if (isKeyboardUp && keyboard.Position.Y > position.Y)
            {
                keyboard.Position = new Vector2(keyboard.Position.X, keyboard.Position.Y - 30);
                inputField.Position = new Vector2(inputField.Position.X, inputField.Position.Y - 30);
            }
            // Keyboard down
            else if (!isKeyboardUp && keyboard.Position.Y < position.Y - 100)
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
