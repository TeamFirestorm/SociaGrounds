﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace SociaGrounds.Model.GUI.Input
{
    public static class STouch
    {
        public static TouchCollection NewTouchLocations { get; set; }

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
    }
}
