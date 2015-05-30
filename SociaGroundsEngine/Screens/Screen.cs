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
        private readonly Texture2D background;

        // List of all the textures of the GUI (button, input field, etc)
        private readonly List<Texture2D> guiTextures;

        protected Screen(List<Texture2D> guiTextures, Texture2D background)
        {
            this.guiTextures = guiTextures;
            this.background = background;
        }

        public Texture2D Background
        {
            get { return background; }
        }

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
