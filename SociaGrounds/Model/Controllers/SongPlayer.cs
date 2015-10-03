using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
using SociaGrounds.Model.Options;

namespace SociaGrounds.Model.Controllers
{
    public static class SongPlayer
    {
        private static readonly List<Song> SONG_LIST;

        static SongPlayer()
        {
            // Initializing songlist
            SONG_LIST = new List<Song>();
        }

        public static void AddSong(Song song)
        {
            SONG_LIST.Add(song);
        }

        public static void PlaySong(int song, bool repeat = false)
        {
            if (song >= SONG_LIST.Count) return;
            if (Config.Instance.MutedMusic) return;

            MediaPlayer.Play(SONG_LIST[song]);
            MediaPlayer.IsRepeating = repeat;
        }
    }
}
