using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SociaGrounds.Model.GUI.Input
{
    public static class SMouse
    {
        public static MouseState OldMouseState { get; set; }

        public static MouseState NewMouseState { get; set; }

        public static ControlReturn IsClicked(Rectangle hitBox)
        {
            // Check if the current mouse position is over hitbox
            if (new Rectangle(NewMouseState.X, NewMouseState.Y, 1, 1).Intersects(hitBox))
            {
                if (NewMouseState.LeftButton == ButtonState.Pressed)
                {
                    return new ControlReturn(true,1);
                }
                return new ControlReturn(false, 1);
            }
            return new ControlReturn(false, 0);
        }
    }
}
