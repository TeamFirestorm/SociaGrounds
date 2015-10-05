using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using SociaGrounds.Model.GUI;
using SociaGrounds.Model.Players;

namespace SociaGrounds.Model.Controllers
{
    public static class Static
    {
        public static string ThisDevice { get; set; }
        public static Texture2D PlayerTexture { get; set; }

        //The current Activated Screen;
        public static ScreenState CurrentScreenState { get; set; }

        //The list containing all the players
        public static List<Player> Players { get; }

        public static List<Button> Buttons { get; }

        //The size of the current screen
        public static Viewport ScreenSize { get; set; }

        static Static()
        {
            Players = new List<Player>();
            Buttons = new List<Button>();
        }

        /// <summary>
        /// CompareById the parameter id to the id of the player
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ForeignPlayer FindForeignPlayerById(int id)
        {
            foreach (Player player in Players)
            {
                if (player.Id == id)
                {
                    return (ForeignPlayer)player;
                }
            }
            return null;
        }
    }
}
