using Microsoft.Xna.Framework;

namespace SociaGrounds.Model.GUI.Input
{
    public class ScreenClick
    {
        public Rectangle Hitbox { get; set; }
        public bool IsTouched { get; set; }

        public ScreenClick(Rectangle hitbox)
        {
            Hitbox = hitbox;
            IsTouched = false;
        }
    }
}
