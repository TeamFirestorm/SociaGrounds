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
            foreach (Asset asset in Map.SolidAssets)
            {
                // If a right collision has been found, return true
                if (rect.TouchLeftOf(asset.Rect))
                {
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
            foreach (Asset asset in Map.SolidAssets)
            {
                // If a right collision has been found, return true
                if (rect.TouchRightOf(asset.Rect))
                {
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
            foreach (Asset asset in Map.SolidAssets)
            {
                // If a right collision has been found, return true
                if (rect.TouchBottomOf(asset.Rect))
                {
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
            foreach (Asset asset in Map.SolidAssets)
            {
                // If a right collision has been found, return true
                if (rect.TouchTopOf(asset.Rect))
                {
                    return true;
                }
            }

            // If no collision has been found, return false
            return false;
        }
    }
}
