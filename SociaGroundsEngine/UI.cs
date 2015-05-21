using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SociaGroundsEngine
{
    class UI
    {
        Vector2 cameraCentre;
        public Vector2 CameraCentre
        {
            get { return cameraCentre; }
            set { cameraCentre = value; }
        }

        public UI()
        {

        }

        public void update(Vector2 position)
        {
            cameraCentre = position;
        }
    }
}
