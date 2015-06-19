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
        /// 
        /// </summary>
        /// <param name="content"></param>
        public Gui(ContentManager content)
        {
            _keyboard = new RealKeyBoard(content);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="position"></param>
        /// <param name="viewport"></param>
        /// <param name="keyState"></param>
        public void Update(Vector2 position, Viewport viewport, KeyboardState keyState)
        {
            _keyboard.Update(position,viewport,keyState);
        }

        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            _keyboard.Draw(spriteBatch);
        }
    }
}
