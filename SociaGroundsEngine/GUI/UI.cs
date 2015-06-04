using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace SociaGroundsEngine.GUI
{
    public class Ui
    {
        // The room menu
        RoomMenu roomMenu;
        
        // Camera centre
        Vector2 cameraCentre;
        public Vector2 CameraCentre
        {
            get { return cameraCentre; }
            set { cameraCentre = value; }
        }

        public Ui(ContentManager content)
        {
            roomMenu = new RoomMenu(content, new Vector2(0, 0));
        }

        public void Update(Vector2 position)
        {
            // Updating the camera centre and the room menu according to the camera centre
            cameraCentre = position;
            roomMenu.Position = new Vector2(cameraCentre.X - 100, cameraCentre.Y + 100);
            roomMenu.update();
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

        public void draw(SpriteBatch spriteBatch)
        {
            roomMenu.draw(spriteBatch);
        }
    }
}
