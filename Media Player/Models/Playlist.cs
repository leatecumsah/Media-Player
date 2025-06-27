using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media_Player.Models
{
    class Playlist
    {
        public int PlaylistId { get; set; }
        public string Name { get; set; }

        public List<VideoFile> Videos { get; set; } = new List<VideoFile>();
    }
}
