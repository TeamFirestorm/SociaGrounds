using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using SociaGrounds.Model.GUI;
using SociaGrounds.Model.Players;

namespace SociaGrounds.Model.Controllers
{
    public static class Static
    {
        public static AKeyBoard Keyboard { get; set; }

        public static Texture2D PlayerTexture { get; set; }
        public static SpriteFont Font { get; set; }

        //The current Activated Screen;
        public static ScreenState CurrentScreenState { get; set; }

        //The list containing all the players
        public static List<CPlayer> Players { get; private set; }

        //The size of the current screen
        public static Viewport ScreenSize { get; set; }

        static Static()
        {
            Players = new List<CPlayer>();
        }
    }
}
