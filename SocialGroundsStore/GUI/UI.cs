using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SocialGroundsStore.GUI
{
    public class Ui
    {
        // The room menu
        private readonly RoomMenu roomMenu;
        private bool isCapsLock;
        private string textBuffer;

        private readonly Keys[] keys = 
        {
            Keys.Q, Keys.W, Keys.E, Keys.R, Keys.T, Keys.Y, Keys.U, Keys.I, Keys.O, Keys.P, 
            Keys.A, Keys.S , Keys.D, Keys.F, Keys.G, Keys.H, Keys.J, Keys.K, Keys.L, 
            Keys.Z, Keys.X, Keys.C, Keys.V, Keys.B, Keys.N, Keys.M, Keys.Space, 
            Keys.NumPad1, Keys.NumPad2, Keys.NumPad3, Keys.NumPad4, Keys.NumPad5, Keys.NumPad6, Keys.NumPad7,
            Keys.NumPad8, Keys.NumPad9, Keys.NumPad0
        };

        private readonly char[] lowerCase =
        {
            'q','w','e','r','t','y','u','i','o','p','a','s','d','f','g','h','j','k','l','z','x','c','v','b','n','m',' ',
            '1','2','3','4','5','6','7','8','9','0'
        };

        private readonly char[] upperCase =
        {
            'Q','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H','J','K','L','Z','X','C','V','B','N','M',' ',
            '!','@','#','$','%','^','&','*','(',')'
        };

        private readonly bool[] isClicked;

        private bool isBackSpace;
        private bool isEnter;
        private bool isCapsLockDown;

        // Camera centre
        private Vector2 cameraCentre;

        //public Vector2 CameraCentre
        //{
        //    get { return cameraCentre; }
        //    set { cameraCentre = value; }
        //}

        public Ui(ContentManager content, Viewport viewport)
        {
            roomMenu = new RoomMenu(content, new Vector2(0, 0), viewport);
            textBuffer = "";
            isCapsLock = false;
            isClicked = new bool[upperCase.Length];
        }

        public void Update(Vector2 position, Viewport viewport)
        {
            // Updating the camera centre and the room menu according to the camera centre
            cameraCentre = position;
            roomMenu.Update(cameraCentre, viewport, textBuffer);
        }

        public void CheckKeyState(KeyboardState keyState)
        {
            bool isUpperCase = keyState.IsKeyDown(Keys.LeftShift) || keyState.IsKeyDown(Keys.RightShift);

            if (keyState.IsKeyDown(Keys.CapsLock))
            {
                isCapsLockDown = true;
            }

            if (keyState.IsKeyDown(Keys.Back))
            {
                if (textBuffer.Length <= 0) return;
                isBackSpace = true;
            }

            if (keyState.IsKeyDown(Keys.Enter))
            {
                isEnter = true;
            }

            for (int i = 0; i < keys.Length -1; i++)
            {
                if (keyState.IsKeyDown(keys[i]))
                {
                    isClicked[i] = true;
                }
            }

            if (keyState.IsKeyUp(Keys.Back))
            {
                if (isBackSpace)
                {
                    isBackSpace = false;
                    char[] text = textBuffer.ToCharArray();
                    textBuffer = new string(text, 0, text.Length - 1);
                }
            }

            if (keyState.IsKeyUp(Keys.Enter))
            {
                if (isEnter)
                {
                    isEnter = false;
                    OnEnter();
                }
            }

            if (keyState.IsKeyUp(Keys.CapsLock))
            {
                isCapsLock = !isCapsLock;
                if (isCapsLock)
                {
                    isUpperCase = true;
                }
            }

            for (int i = 0; i < keys.Length - 1; i++)
            {
                if (keyState.IsKeyUp(keys[i]))
                {
                    if (isClicked[i])
                    {
                        isClicked[i] = false;
                        AddText(isUpperCase ? upperCase[i] : lowerCase[i]);
                    }
                }
            }
        }

        private void AddText(char value)
        {
            textBuffer = textBuffer + value;
        }

        public void OnEnter()
        {
            textBuffer = "";
        }

        public void CheckMouseDown(MouseState mouseState)
        {
            roomMenu.CheckMouseDown(mouseState);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            roomMenu.Draw(spriteBatch);
        }
    }
}
