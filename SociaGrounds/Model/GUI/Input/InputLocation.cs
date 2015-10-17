using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace SociaGrounds.Model.GUI.Input
{
    public static class InputLocation
    {
        public static void SetMovementTouch(Viewport viewport)
        {
            int width = viewport.Width/5;
            int height = viewport.Height/4;


            //initialize the player touch movement
            // starting left, top, right, bottom
            MovementTouch = new []
            {
                new ScreenClick(new Rectangle(0,height,width,height * 2)),
                new ScreenClick(new Rectangle(width,0,width * 3,height)),
                new ScreenClick(new Rectangle(width * 4, height,width,height * 2)),
                new ScreenClick(new Rectangle(width,height * 3, width * 3, height)),
            };
        }

        public static MouseState OldMouseState { get; set; }

        public static MouseState NewMouseState { get; set; }

        public static TouchCollection NewTouchLocations { get; set; }

        public static ScreenClick[] MovementTouch { get; private set; }

        public static ControlReturn IsClicked(Rectangle hitBox)
        {
            // Check if the current mouse position is over hitbox
            if (new Rectangle(NewMouseState.X, NewMouseState.Y, 1, 1).Intersects(hitBox))
            {
                if (NewMouseState.LeftButton == ButtonState.Pressed)
                {
                    return new ControlReturn(true, 1);
                }
                return new ControlReturn(false, 1);
            }
            return new ControlReturn(false, 0);
        }

        public static ControlReturn IsTouchReleased(Rectangle hitBox, ref bool isTouched)
        {
            if (NewTouchLocations.Count == 0 && isTouched)
            {
                isTouched = false;
                return new ControlReturn(true, 0);
            }

            // Loop through all the locations where touch is possible
            foreach (TouchLocation touch in NewTouchLocations)
            {
                // Check if the position is pressed within the button
                if (new Rectangle((int)touch.Position.X, (int)touch.Position.Y, 1, 1).Intersects(hitBox))
                {
                    //_buttonState = 1;
                    isTouched = true;

                    return new ControlReturn(false, 1);
                }
            }

            return new ControlReturn(false, 0);
        }

        public static bool IsTouchDown(int index)
        {
            ScreenClick screenClick = MovementTouch[index];

            if (NewTouchLocations.Count == 0 && screenClick.IsTouched)
            {
                screenClick.IsTouched = false;
                return false;
            }

            // Loop through all the locations where touch is possible
            foreach (TouchLocation touch in NewTouchLocations)
            {
                // Check if the position is pressed within the button
                if (new Rectangle((int)touch.Position.X, (int)touch.Position.Y, 1, 1).Intersects(screenClick.Hitbox))
                {
                    //_buttonState = 1;
                    screenClick.IsTouched = true;

                    return true;
                }
            }

            return false;
        }
    }
}
