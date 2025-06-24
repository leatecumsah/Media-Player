using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Modell sagt wpf, welche Datenstruktur sie verarbeiten soll

namespace Media_Player.Models
{
    class MediaItem
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string filepath { get; set; }
        public string CoverImagePath { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
