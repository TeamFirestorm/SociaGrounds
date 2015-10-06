using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SociaGrounds.Model.Players
{
    public static class StaticPlayer
    {
        public static Texture2D PlayerTexture { get; set; }
        public static MyPlayer MyPlayer { get; set; }
        //The list containing all the players
        public static List<ForeignPlayer> ForeignPlayers { get; }

        static StaticPlayer()
        {
            ForeignPlayers = new List<ForeignPlayer>();
        }

        /// <summary>
        /// CompareById the parameter id to the id of the player
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ForeignPlayer FindForeignPlayerById(int id)
        {
            foreach (ForeignPlayer player in ForeignPlayers)
            {
                if (player.Id == id)
                {
                    return player;
                }
            }
            return null;
        }

        public static void DrawPlayers(SpriteBatch spriteBatch)
        {
            MyPlayer.Draw(spriteBatch);
            foreach (var player in ForeignPlayers)
            {
                player.Draw(spriteBatch);
            }
        }

        public static void DrawPlayerText(SpriteBatch spriteBatch)
        {
            MyPlayer.DrawText(spriteBatch);
            foreach (var player in ForeignPlayers)
            {
                player.DrawText(spriteBatch);
            }
        }

    public static void UpdateForeignPlayers(GameTime gameTime)
        {
            foreach (ForeignPlayer foreign in ForeignPlayers)
            {
                foreign.Update(gameTime);
            }
        }
    }
}
