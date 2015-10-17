using System.Diagnostics;
using Microsoft.Xna.Framework;
using SociaGrounds.Model.World;

namespace SociaGrounds.Model.Players
{
    public static class CollisionDetection
    {
        // Check if the player has a collision on his right side
        public static bool IsCollidingRight(Rectangle playerHitbox)
        {
            // Check all the solid assets in the map
            foreach (SolidAsset asset in Map.SolidAssets)
            {
                // If a right collision has been found, return true
                if (playerHitbox.TouchLeftOf(asset.HitBox))
                {
                    Debug.WriteLine(asset + " Right");
                    return true;
                }
            }

            // If no collision has been found, return false
            return false;
        }

        // Check if the player has a collision on his left side
        public static bool IsCollidingLeft(Rectangle playerHitbox)
        {
            // Check all the solid assets in the map
            foreach (SolidAsset asset in Map.SolidAssets)
            {
                // If a right collision has been found, return true
                if (playerHitbox.TouchRightOf(asset.HitBox))
                {
                    Debug.WriteLine(asset + " Left");
                    return true;
                }
            }

            // If no collision has been found, return false
            return false;
        }

        // Check if the player has a collision on his top side
        public static bool IsCollidingTop(Rectangle playerHitbox)
        {

            // Check all the solid assets in the map
            foreach (SolidAsset asset in Map.SolidAssets)
            {
                // If a right collision has been found, return true
                if (playerHitbox.TouchBottomOf(asset.HitBox))
                {
                    Debug.WriteLine(asset + " Top");
                    return true;
                }
            }

            // If no collision has been found, return false
            return false;
        }

        // Check if the player has a collision on his bottom side
        public static bool IsCollidingBottom(Rectangle playerHitbox)
        {
            // Check all the solid assets in the map
            foreach (SolidAsset asset in Map.SolidAssets)
            {
                // If a right collision has been found, return true
                if (playerHitbox.TouchTopOf(asset.HitBox))
                {
                    Debug.WriteLine(asset + " Bottom");
                    return true;
                }
            }

            // If no collision has been found, return false
            return false;
        }

        public static bool TouchTopOf(this Rectangle playerHitbox, Rectangle assetHitBox)
        {
            return (playerHitbox.Bottom >= assetHitBox.Top - 1
                    && playerHitbox.Bottom <= assetHitBox.Top + (assetHitBox.Height / 2)
                    && playerHitbox.Right >= assetHitBox.Left + (assetHitBox.Width / 5)
                    && playerHitbox.Left <= assetHitBox.Right - (assetHitBox.Width / 5));
        }

        public static bool TouchBottomOf(this Rectangle playerHitbox, Rectangle assetHitBox)
        {
            return (playerHitbox.Top <= assetHitBox.Bottom + (assetHitBox.Height / 5)
                    && playerHitbox.Top >= assetHitBox.Bottom
                    && playerHitbox.Right >= assetHitBox.Left + (assetHitBox.Width / 5)
                    && playerHitbox.Left <= assetHitBox.Right - (assetHitBox.Width / 5));
        }

        public static bool TouchLeftOf(this Rectangle playerHitbox, Rectangle assetHitBox)
        {
            return (playerHitbox.Right <= assetHitBox.Right
                    && playerHitbox.Right >= assetHitBox.Left - 4
                    && playerHitbox.Top <= assetHitBox.Bottom - (assetHitBox.Width / 4)
                    && playerHitbox.Bottom >= assetHitBox.Top + (assetHitBox.Width / 4));
        }

        public static bool TouchRightOf(this Rectangle playerHitbox, Rectangle assetHitBox)
        {
            return (playerHitbox.Left >= assetHitBox.Left
                    && playerHitbox.Left <= assetHitBox.Right + 2
                    && playerHitbox.Top <= assetHitBox.Bottom - (assetHitBox.Width / 4)
                    && playerHitbox.Bottom >= assetHitBox.Top + (assetHitBox.Width / 4));
        }
    }
}
