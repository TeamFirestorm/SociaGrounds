using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SociaGroundsEngine.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SociaGroundsEngine
{
    class Camera
    {
        private Matrix transformPrivate;

        public Matrix tranformPublic
        {
            get
            {
                return transformPrivate;
            }
        }

        public Vector2 centre;
        private Viewport viewport;

        public Camera(Viewport newView)
        {
            viewport = newView;
        }

        public void Update(Viewport viewport, Vector2 position, Map map)
        {
            if (position.X < viewport.Width / 2)
            {
                centre.X = viewport.Width / 2;
            }
            else if (position.X > map.MapWidth - (viewport.Width / 2))
            {
                centre.X = map.MapWidth - (viewport.Width / 2);
            }
            else
            {
                centre.X = position.X;
            }

            if (position.Y < viewport.Height / 2)
            {
                centre.Y = viewport.Height / 2;
            }
            else if (position.Y > map.MapHeight - (viewport.Height / 2) - 64)
            {
                centre.Y = map.MapHeight - (viewport.Height / 2) - 64;
            }
            else
            {
                centre.Y = position.Y;
            }

            //centre.X = position.X + 40;
            //centre.Y = position.Y + 40;

            transformPrivate = Matrix.CreateTranslation(-centre.X + (viewport.Width / 2), -centre.Y + (viewport.Height / 2), 0);
        }
    }
}
