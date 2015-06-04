using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace SociaGroundsEngine.GUI
{
    public class Ui
    {
        Vector2 cameraCentre;
        public Vector2 CameraCentre
        {
            get { return cameraCentre; }
            set { cameraCentre = value; }
        }

        public void Update(Vector2 position)
        {
            cameraCentre = position;
        }

        // Input checks for touching up, down, left or right
        public bool TouchUp(Viewport viewPort)
        {
            // Loop through all the locations where touch is possible
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

        public bool TouchDown(Viewport viewPort)
        {
            // Loop through all the locations where touch is possible
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

        public bool TouchLeft(Viewport viewPort)
        {
            // Loop through all the locations where touch is possible
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

        public bool TouchRight(Viewport viewPort)
        {
            // Loop through all the locations where touch is possible
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
