using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MusicBox
{
    public class Song
    {
        private int song_id;
        private string song_name;
        private string song_path;
        private DateTime publish_date;
        private string song_span;
        private string coverPath;
        public MediaPlayer MusicPlayer = new MediaPlayer();

        public int Song_id { get { return song_id; } set { song_id = value; } }
        public string Song_name { get { return song_name; } set { song_name = value; } }
        public string Song_path { get { return song_path; } set { song_path = value; } }
        public DateTime Publish_date { get { return publish_date; } set { publish_date = value; } }
        public string Song_span { get { return song_span; } set { song_span = value; } }
        public string CoverPath { get { return coverPath; } set { coverPath = value; } }
        public int PlayWithAbsolutePath(string path)
        {
            
            if (File.Exists(path))
            {
                MusicPlayer.Open(new Uri(path, UriKind.Absolute));
                MusicPlayer.Play();
            }
            else
            {
                return -1;
            }
            return 1;
        }

        public int PausePlayer()
        {
            MusicPlayer.Pause();
            return 1;
        }
    }
}
