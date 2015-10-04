using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using SociaGrounds.Model.GUI;

namespace SociaGrounds.Model.KeyBoards
{
    /// <summary>
    /// Class is used to connect the windows phone & desktop keyboards.
    /// Making it easier to migrate between the platforms.
    /// </summary>
    public abstract class AKeyBoard
    {
        protected bool _IsUppercase;
        protected bool _IsCapsLock;

        protected static readonly Keys[] AllKeys =
        {
            Keys.Q, Keys.W, Keys.E, Keys.R, Keys.T, Keys.Y, Keys.U, Keys.I, Keys.O, Keys.P,
            Keys.A, Keys.S , Keys.D, Keys.F, Keys.G, Keys.H, Keys.J, Keys.K, Keys.L,
            Keys.Z, Keys.X, Keys.C, Keys.V, Keys.B, Keys.N, Keys.M, Keys.Space,
            Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7,
            Keys.D8, Keys.D9, Keys.D0
        };

        protected static readonly char[] UpperCase =
        {
            'Q','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H','J','K','L','Z','X','C','V','B','N','M',' ',
            '!','@','#','$','%','^','&','*','(',')'
        };

        protected static readonly char[] UpperCapsCase =
        {
            'Q','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H','J','K','L','Z','X','C','V','B','N','M',' ',
            '1','2','3','4','5','6','7','8','9','0'
        };

        protected static readonly char[] LowerCase =
        {
            'q','w','e','r','t','y','u','i','o','p','a','s','d','f','g','h','j','k','l','z','x','c','v','b','n','m',' ',
            '1','2','3','4','5','6','7','8','9','0'
        };

        public string TextBuffer { get; protected set; }
        
        public KeyboardState OldKeyboardState;

        protected AKeyBoard()
        {
            TextBuffer = "";
            _IsUppercase = false;
            _IsCapsLock = false;
        }

        public abstract void CheckKeyState(KeyboardState state = default(KeyboardState));

        /// <summary>
        /// Method that adds text to the textbuffer
        /// </summary>
        /// <param name="value">The value that needs to be added</param>
        protected virtual void AddText(char value)
        {
            TextBuffer = TextBuffer + value;
        }
    }
}
