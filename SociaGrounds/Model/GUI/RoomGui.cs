using Microsoft.Xna.Framework;
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
        public RoomGui()
        {
            //the stats for the inputfield
            float scale = 0.5f;
            Vector2 sprite = new Vector2(20 * scale, 120 * scale);
            int width = (int) ((Static.ScreenSize.Width/2d)/sprite.X);
            Vector2 position = new Vector2(((Static.ScreenSize.Width) - (sprite.X * width))/2, Static.ScreenSize.Height - sprite.Y - 80);



            // Initialize controls
            _inputField = new InputField(position, width, scale, Fonts.LargeFont);

            string text = "Show/Hide";

            _showHideButton = new Button(new Vector2(20, position.Y), text, scale, text.Length, Fonts.SmallFont);

            //set the virtualkeyboard to not show
            _isKeyboardUp = false;
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
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            _inputField.Draw(spriteBatch);
            _showHideButton.Draw(spriteBatch);
        }
    }
}
