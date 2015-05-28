using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SociaGroundsEngine
{
    public class UI
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


        // Input checks for touching up, down, left or right
        public bool touchUp(Viewport viewPort)
        {
            // Loop through all the locations where touch is possible
            TouchCollection locationArray = TouchPanel.GetState();
            foreach (TouchLocation touch in TouchPanel.GetState())
            {
                // Check if the position is touched within the upper area
                if (touch.State == TouchLocationState.Moved && 
                    touch.Position.X <= viewPort.Width * 0.75 &&
                    touch.Position.X >= viewPort.Width * 0.25 &&
                    touch.Position.Y <= viewPort.Height * 0.25)
                {
                    return true;
                }
            }
            return false;
        }

        public bool touchDown(Viewport viewPort)
        {
            // Loop through all the locations where touch is possible
            TouchCollection locationArray = TouchPanel.GetState();
            foreach (TouchLocation touch in TouchPanel.GetState())
            {
                // Check if the position is touched within the upper area
                if (touch.State == TouchLocationState.Moved &&
                    touch.Position.X <= viewPort.Width * 0.75 &&
                    touch.Position.X >= viewPort.Width * 0.25 &&
                    touch.Position.Y >= viewPort.Height * 0.75)
                {
                    return true;
                }
            }
            return false;
        }

        public bool touchLeft(Viewport viewPort)
        {
            // Loop through all the locations where touch is possible
            TouchCollection locationArray = TouchPanel.GetState();
            foreach (TouchLocation touch in TouchPanel.GetState())
            {
                // Check if the position is touched within the upper area
                if (touch.State == TouchLocationState.Moved &&
                    touch.Position.X <= viewPort.Width * 0.25 &&
                    touch.Position.Y >= viewPort.Height * 0.25 &&
                    touch.Position.Y <= viewPort.Height * 0.75)
                {
                    return true;
                }
            }
            return false;
        }

        public bool touchRight(Viewport viewPort)
        {
            // Loop through all the locations where touch is possible
            TouchCollection locationArray = TouchPanel.GetState();
            foreach (TouchLocation touch in TouchPanel.GetState())
            {
                // Check if the position is touched within the upper area
                if (touch.State == TouchLocationState.Moved &&
                    touch.Position.X >= viewPort.Width * 0.75 &&
                    touch.Position.Y >= viewPort.Height * 0.25 &&
                    touch.Position.Y <= viewPort.Height * 0.75)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
