using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SocialGroundsStore.GUI;
using SocialGroundsStore.World;

namespace SocialGroundsStore

    class Camera
    {
        /// <summary>
        /// Camera class keeps track of the player
        /// </summary>
        public Matrix TranformPublic { get; private set; }

        private Vector2 _centre;

        public void Update(Viewport viewport, Vector2 position, Map map, Gui ui, KeyboardState keyState)
        {
            // If the camera is on the left side of the map, stop the horizontal camera movement
            if (position.X < map.StartPosition.X + (viewport.Width / 4f))
            {
                _centre.X = map.StartPosition.X + (viewport.Width / 4f);
            }
            // If the camera is on the right side of the map , stop the horizontal camera movement
            else if (position.X > map.StartPosition.X + (map.MapWidth - (viewport.Width / 3f)))
            {
                _centre.X = map.StartPosition.X + (map.MapWidth - (viewport.Width / 3f));
            }
            // Horizontal camera movement
            else
            {
                _centre.X = position.X;
            }

            // If the camera is on the top of the map, stop the vertical camera movement
            if (position.Y < map.StartPosition.Y + (viewport.Height / 4f))
            {
                _centre.Y = map.StartPosition.Y + (viewport.Height / 4f);
            }
            // If the camera is on the bottom of the map, stop the vertical camera movement
            else if (position.Y > (map.StartPosition.Y + map.MapHeight) - (viewport.Height / 3f))
            {
                _centre.Y = (map.StartPosition.Y + map.MapHeight) - (viewport.Height / 3f);
            }
            // Vertical camera movement
            else
            {
                _centre.Y = position.Y;
            }

            // Camera translation
            TranformPublic = Matrix.CreateTranslation(new Vector3(-_centre.X + (viewport.Height / 2.5f), -_centre.Y + (viewport.Width / 8f), 0)) * Matrix.CreateScale(2.0f, 2.0f, 0);

            ui.Update(_centre, viewport, keyState);
        }
    }
}
