using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SociaGroundsEngine.Screens
{
    public abstract class Screen
    {
        Texture2D background;
        public Texture2D Background
        {
            get { return background; }
        }

        // List of all the textures of the GUI (button, input field, etc)
        List<Texture2D> guiTextures;
        public List<Texture2D> GuiTextures
        {
            get { return guiTextures; }
        }

        // Method to update the screen frequently
        public abstract void update();

        // Method to draw the screen frequently
        public abstract void draw(SpriteBatch spriteBatch);
    }
}
