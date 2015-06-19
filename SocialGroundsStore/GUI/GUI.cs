using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SocialGroundsStore.GUI
{
    public class Gui
    {
        private readonly RealKeyBoard _keyboard;

        /// <summary>
        /// This class is used to build the user interface of the application
        /// </summary>
        /// <param name="content">The contents in this parameter will be used to draw the GUI</param>
        public Gui(ContentManager content)
        {
            _keyboard = new RealKeyBoard(content);
        }

        /// <summary>
        /// Update method for the keyboard
        /// </summary>
        /// <param name="position">Position of the key pressed</param>
        /// <param name="viewport">Your screen(resolution)</param>
        /// <param name="keyState">All keys in the keyboard</param>
        public void Update(Vector2 position, Viewport viewport, KeyboardState keyState)
        {
            _keyboard.Update(position,viewport,keyState);
        }

        /// <summary>
        /// Draw the text and show it above the corresponding player's head
        /// </summary>
        /// <param name="spriteBatch">Contains the text for the chat</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            _keyboard.Draw(spriteBatch);
        }
    }
}
