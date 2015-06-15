using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SocialGroundsStore.GUI
{
    class RoomMenu
    {
        private readonly Button showHideButton;
        private readonly SociaInputfield inputField;
        private bool isKeyboardUp;

        float inputFieldHeight;

        public RoomMenu(ContentManager content, Vector2 position, Viewport viewport)
        {
            isKeyboardUp = false;
            inputFieldHeight = 50;
            showHideButton = new Button(content, new Vector2(viewport.X + 100, viewport.Height - 100), "Show/Hide ", 0.4f);
            inputField = new SociaInputfield(content, new Vector2(position.X + 100, position.Y), 10, 0.08f);
        }

        public void Update(Vector2 newPosition, Viewport viewport, string text)
        {
            // Position updates
            showHideButton.Position = new Vector2(newPosition.X - (viewport.Width / 4.6f), newPosition.Y + (viewport.Height / 4.7f));
            //keyboard.Position = new Vector2(newPosition.X - (viewport.Width / 11f), newPosition.Y + (viewport.Height / keyboardHeight));
            inputField.Position = new Vector2(newPosition.X - (viewport.Width / 11f), newPosition.Y + (viewport.Height / inputFieldHeight));

            // Input field update
            inputField.update(text);
            //keyboard.update(viewport);
        }


        public void CheckMouseDown(MouseState mouseState)
        {
            if (mouseState.LeftButton != ButtonState.Pressed) return;

            // Check if the Show/Hide button is clicked
            if (showHideButton.IsClicked(mouseState))
            {
                // Toggle on
                if (!isKeyboardUp)
                {
                    isKeyboardUp = true;
                }
                // Toggle off
                else if (isKeyboardUp)
                {
                    isKeyboardUp = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            showHideButton.Draw(spriteBatch);
            inputField.draw(spriteBatch);
        }
    }
}
