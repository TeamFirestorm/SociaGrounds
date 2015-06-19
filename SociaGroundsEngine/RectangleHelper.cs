using System;
using Microsoft.Xna.Framework;

namespace SociaGroundsEngine
{
    static class RectangleHelper
    {
        public static bool TouchTopOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Bottom >= r2.Top - 1 &&
                    r1.Bottom <= r2.Top + (r2.Height / 2) &&
                    r1.Right >= r2.Left + (r2.Width / 5) &&
                    r1.Left <= r2.Right - (r2.Width / 5));
        }

        public static bool TouchBottomOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Top <= r2.Bottom + (r2.Height / 5) &&
                   r1.Top >= r2.Bottom - 1 &&
                   r1.Right >= r2.Left + (r2.Width / 5) &&
                   r1.Left <= r2.Right - (r2.Width / 5));
        }

        public static bool TouchLeftOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Right <= r2.Right &&
                    r1.Right >= r2.Left &&
                    r1.Top <= r2.Bottom - (r2.Width / 4) &&
                    r1.Bottom >= r2.Top + (r2.Width / 4));
        }

        public static bool TouchRightOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Left >= r2.Left &&
                   r1.Left <= r2.Right + 5 &&
                   r1.Top <= r2.Bottom - (r2.Width / 4) &&
                   r1.Bottom >= r2.Top + (r2.Width / 4));
        }

        // Checks if the pixels collide each other
        public static bool IntersectsPixel(Rectangle rect1, Color[] data1,
                                           Rectangle rect2, Color[] data2)
        {
            int top = Math.Max(rect1.Top, rect2.Top);
            int bottom = Math.Min(rect1.Bottom, rect2.Bottom);
            int left = Math.Max(rect1.Left, rect2.Left);
            int right = Math.Min(rect1.Right, rect2.Right);

            // Loop through the pixels to check for a collision
            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    Color color1 = data1[(x - rect1.Left) + (y - rect1.Top) * rect1.Width];
                    Color color2 = data1[(x - rect2.Left) + (y - rect2.Top) * rect2.Width];

                    // Check if the colors overlap each other
                    // If so, return true
                    if (color1.A != 0 && color2.A != 0)
                    {
                        return true;
                    }
                }
            }

            // If collision has not been found, return false
            return false;
        }
    }
}
