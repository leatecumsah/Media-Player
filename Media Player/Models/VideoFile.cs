using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Media_Player.Models
{
    public class VideoFile
    {
        public string FilePath { get; set; }
        public string Title { get; set; }
        public TimeSpan Duration { get; set; }
        public string CoverimagePath {get;set;}
        public int Id { get; set; }

        public VideoFile(string filePath, string title, TimeSpan duration, int id , string coverimagePath)
        {
            FilePath = filePath;
            Title = title;
            Duration = duration;
            Id = id;
            CoverimagePath = coverimagePath;

        }public VideoFile() { }
    }
    
    }
