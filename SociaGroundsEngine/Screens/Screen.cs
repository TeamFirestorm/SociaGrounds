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
        // Method to update the screen frequently
        public abstract void Update();

        // Method to draw the screen frequently
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
