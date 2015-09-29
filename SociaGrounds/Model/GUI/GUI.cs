using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SociaGrounds.Model.Controllers;

namespace SociaGrounds.Model.GUI
{
    public class Gui
    {
        private readonly InputField _inputField;
        private readonly float _inputFieldHeight;

        /// <summary>
        /// This class is used to build the user interface of the application
        /// </summary>
        /// <param name="content">The contents in this parameter will be used to draw the GUI</param>
        public Gui(ContentManager content)
        {
            _inputFieldHeight = 4.7f;
            _inputField = new InputField(content, new Vector2(100, 0), 10, 0.08f);
        }

        /// <summary>
        /// Update method for the keyboard
        /// </summary>
        /// <param name="position">Position of the key pressed</param>
        /// <param name="viewport">Your screen(resolution)</param>
        public void Update(Vector2 position, Viewport viewport)
        {
            _inputField.Position = new Vector2(position.X - (viewport.Width / 11f), position.Y + (viewport.Height / _inputFieldHeight));
            _inputField.Update(Static.Keyboard.TextBuffer);
        }

        /// <summary>
        /// Draw the text and show it above the corresponding player's head
        /// </summary>
        /// <param name="spriteBatch">Contains the text for the chat</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            _inputField.Draw(spriteBatch);
        }
    }
}
