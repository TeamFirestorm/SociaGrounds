using System.Diagnostics;
using Microsoft.Xna.Framework;
using SociaGrounds.Model.World;

namespace SociaGrounds.Model.Players
{
    public static class CollisionDetection
    {
        // Check if the player has a collision on his right side
        public static bool IsCollidingRight(Rectangle rect)
        {
            // Check all the solid assets in the map
            foreach (SolidAsset asset in Map.SolidAssets)
            {
                // If a right collision has been found, return true
                if (rect.TouchLeftOf(asset.Rect))
                {
                    Debug.WriteLine(asset + " Right");
                    return true;
                }
            }

            // If no collision has been found, return false
            return false;
        }

        // Check if the player has a collision on his left side
        public static bool IsCollidingLeft(Rectangle rect)
        {
            // Check all the solid assets in the map
            foreach (SolidAsset asset in Map.SolidAssets)
            {
                // If a right collision has been found, return true
                if (rect.TouchRightOf(asset.Rect))
                {
                    Debug.WriteLine(asset + " Left");
                    return true;
                }
            }

            // If no collision has been found, return false
            return false;
        }

        // Check if the player has a collision on his top side
        public static bool IsCollidingTop(Rectangle rect)
        {

            // Check all the solid assets in the map
            foreach (SolidAsset asset in Map.SolidAssets)
            {
                // If a right collision has been found, return true
                if (rect.TouchBottomOf(asset.Rect))
                {
                    Debug.WriteLine(asset + " Top");
                    return true;
                }
            }

            // If no collision has been found, return false
            return false;
        }

        // Check if the player has a collision on his bottom side
        public static bool IsCollidingBottom(Rectangle rect)
        {
            // Check all the solid assets in the map
            foreach (SolidAsset asset in Map.SolidAssets)
            {
                // If a right collision has been found, return true
                if (rect.TouchTopOf(asset.Rect))
                {
                    Debug.WriteLine(asset + " Bottom");
                    return true;
                }
            }

            // If no collision has been found, return false
            return false;
        }

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
    }
}
