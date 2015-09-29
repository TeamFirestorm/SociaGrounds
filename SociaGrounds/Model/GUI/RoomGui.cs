using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SociaGrounds.Model.Controllers;

namespace SociaGrounds.Model.GUI
{
    public class RoomGui
    {
        private readonly InputField _inputField;
        private readonly Button _showHideButton;

        // Boolean to check if the keyboard is up
        private bool _isKeyboardUp;

        /// <summary>
        /// This class is used to build the user interface of the application
        /// </summary>
        /// <param name="content">The contents in this parameter will be used to draw the GUI</param>
        public RoomGui(ContentManager content)
        {
            _inputField = new InputField(content, Static.ScreenSize, 10, 0.2f);

            _isKeyboardUp = false;

            // Initialize
            _showHideButton = new Button(content, Static.ScreenSize, "Show/Hide ", 0.4f);
        }

        /// <summary>
        /// Update method for the keyboard
        /// </summary>
        public void Update()
        {
            _inputField.Update(Static.Keyboard.TextBuffer);

            if (_showHideButton.CheckButtonSelected())
            {
                _isKeyboardUp = !_isKeyboardUp;
            }
        }

        /// <summary>
        /// Draw the text and show it above the corresponding player's head
        /// </summary>
        /// <param name="spriteBatch">Contains the text for the chat</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            _inputField.Draw(spriteBatch);
            _showHideButton.Draw(spriteBatch);
        }
    }
}
