﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SociaGroundsEngine.World;

namespace SociaGroundsEngine
{
    class Camera
    {
        private Matrix transformPrivate;

        public Matrix TranformPublic
        {
            get
            {
                return transformPrivate;
            }
        }

        public Vector2 centre;

        public void Update(Viewport viewport, Vector2 position, Map map)
        {
            // If the camera is on the left side of the map, stop the horizontal camera movement
            if (position.X < map.StartPosition.X + (viewport.Width / 3))
            {
                centre.X = map.StartPosition.X + (viewport.Width / 3);
            }
            // If the camera is on the right side of the map , stop the horizontal camera movement
            if (position.X > map.StartPosition.X + (map.MapWidth - (viewport.Width / 3)))
            {
                centre.X = map.StartPosition.X + (map.MapWidth - (viewport.Width / 3));
            }
            // Horizontal camera movement
            else if (position.X > map.StartPosition.X + (viewport.Width / 3) && position.X < map.StartPosition.X + (map.MapWidth - (viewport.Width / 3)))
            {
                centre.X = position.X;
            }

            // If the camera is on the top of the map, stop the vertical camera movement
            if (position.Y < map.StartPosition.Y + (viewport.Height / 2))
            {
                centre.Y = map.StartPosition.Y + (viewport.Height / 2);
            }
            // If the camera is on the bottom of the map, stop the vertical camera movement
            else if (position.Y > (map.StartPosition.Y + map.MapHeight) - (viewport.Height / 4))
            {
                centre.Y = (map.StartPosition.Y + map.MapHeight) - (viewport.Height / 4);
            }
            // Vertical camera movement
            else if (position.Y > map.StartPosition.Y + (viewport.Height / 2) && position.Y < (map.StartPosition.Y + map.MapHeight) - (viewport.Height / 4))
            {
                centre.Y = position.Y;
            }

            // Camera translation
            transformPrivate = Matrix.CreateTranslation(new Vector3(-centre.X + (viewport.Height / 2.5f), -centre.Y + (viewport.Width / 8), 0)) * Matrix.CreateScale(2.0f, 2.0f, 0);
        }
    }
}
