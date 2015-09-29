using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;

namespace SociaGrounds.Model.Controllers
{
    public static class SongPlayer
    {
        private static readonly List<Song> SongList;

        static SongPlayer()
        {
            // Initializing songlist
            SongList = new List<Song>();
        }

        public static void AddSong(Song song)
        {
            SongList.Add(song);
        }

        public static void PlaySong(int song)
        {
            if (song >= SongList.Count) return;

            MediaPlayer.Play(SongList[song]);
            MediaPlayer.IsRepeating = false;
        }

        public static void PlaySongRepeat(int song)
        {
            if (song >= SongList.Count) return;

            MediaPlayer.Play(SongList[song]);
            MediaPlayer.IsRepeating = true;
        }
    }
}
