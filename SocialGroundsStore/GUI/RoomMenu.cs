using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SocialGroundsStore.GUI
{
    class RoomMenu
    {
        private readonly Button _showHideButton;
        private readonly SociaInputfield _inputField;
        private bool _isKeyboardUp;

        private readonly float _inputFieldHeight;

        public RoomMenu(ContentManager content, Vector2 position, Viewport viewport)
        {
            _isKeyboardUp = false;
            _inputFieldHeight = 4.7f;
            _showHideButton = new Button(content, new Vector2(viewport.X + 100, viewport.Height - 100), "Show/Hide ", 0.4f);
            _inputField = new SociaInputfield(content, new Vector2(position.X + 100, position.Y), 10, 0.08f);
        }

        public void Update(Vector2 newPosition, Viewport viewport, string text)
        {
            // Position updates
            //showHideButton.Position = new Vector2(newPosition.X - (viewport.Width / 4.6f), newPosition.Y + (viewport.Height / 4.7f));
            _inputField.Position = new Vector2(newPosition.X - (viewport.Width / 11f), newPosition.Y + (viewport.Height / _inputFieldHeight));

            // Input field update
            _inputField.update(text);
            //keyboard.update(viewport);
        }

        public void CheckMouseDown(MouseState mouseState)
        {
            if (mouseState.LeftButton != ButtonState.Pressed) return;

            // Check if the Show/Hide button is clicked
            if (_showHideButton.IsClicked(mouseState))
            {
                // Toggle on
                if (!_isKeyboardUp)
                {
                    _isKeyboardUp = true;
                }
                // Toggle off
                else if (_isKeyboardUp)
                {
                    _isKeyboardUp = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //_showHideButton.Draw(spriteBatch);
            _inputField.draw(spriteBatch);
        }
    }
}
