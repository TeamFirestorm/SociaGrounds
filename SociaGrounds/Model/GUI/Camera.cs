using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SociaGrounds.Model.World;

namespace SociaGrounds.Model.GUI
{
    public class Camera
    {
        /// <summary>
        /// Camera class keeps track of the player
        /// </summary>
        public Matrix TranformPublic { get; private set; }

        private Vector2 _centre;

        public void Update(Viewport viewport, Vector2 position, Map map)
        {
            // If the camera is on the left side of the map, stop the horizontal camera movement
            if (position.X < map.StartPosition.X + viewport.Width / 2f)
            {
                _centre.X = map.StartPosition.X + viewport.Width / 2f;
            }
            // If the camera is on the right side of the map , stop the horizontal camera movement
            else if (position.X > map.StartPosition.X + Map.MapWidth - viewport.Width / 2f)
            {
                _centre.X = map.StartPosition.X + (Map.MapWidth - viewport.Width / 2f);
            }
            // Horizontal camera movement
            else
            {
                _centre.X = position.X;
            }

            // If the camera is on the top of the map, stop the vertical camera movement
            if (position.Y < map.StartPosition.Y + viewport.Height / 2f)
            {
                _centre.Y = map.StartPosition.Y + viewport.Height / 2f;
            }
            // If the camera is on the bottom of the map, stop the vertical camera movement
            else if (position.Y > map.StartPosition.Y + Map.MapHeight - viewport.Height / 2f)
            {
                _centre.Y = map.StartPosition.Y + Map.MapHeight - viewport.Height / 2f;
            }
            // Vertical camera movement
            else
            {
                _centre.Y = position.Y;
            }

            // Camera translation
            TranformPublic = Matrix.CreateTranslation(new Vector3(-_centre.X + viewport.Width / 2f, -_centre.Y + viewport.Height / 2f, 0));
        }
    }
}
