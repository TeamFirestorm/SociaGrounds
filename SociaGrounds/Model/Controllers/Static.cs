using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using SociaGrounds.Model.GUI.Controls;

namespace SociaGrounds.Model.Controllers
{
    public static class Static
    {
        public const int SendTime = 50;
        public static string ThisDevice { get; set; }
        //The current Activated Screen;
        public static ScreenState CurrentScreenState { get; set; }
        public static List<Button> KeyBoardButtons { get; }
        //The size of the current screen
        public static Viewport ScreenSize { get; set; }

        static Static()
        {

            KeyBoardButtons = new List<Button>();
        }
    }
}
