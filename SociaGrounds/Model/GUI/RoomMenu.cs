using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SociaGrounds.Model.GUI
{
    public class RoomMenu
    {
        private readonly Button _showHideButton;

        // Boolean to check if the keyboard is up
        private bool _isKeyboardUp;

        public RoomMenu(ContentManager content, Vector2 position)
        {
            _isKeyboardUp = false;

            // Initialize
            _showHideButton = new Button(content, position, "Show/Hide ", 0.4f,new Rectangle());
        }

        public void Update(Vector2 newPosition, Viewport viewport)
        {
            //_showHideButton.Position = newPosition;

            if (_showHideButton.CheckButtonSelected())
            {
                _isKeyboardUp = !_isKeyboardUp;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _showHideButton.Draw(spriteBatch);
        }
    }
}
