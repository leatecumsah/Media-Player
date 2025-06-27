using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;   
using System.ComponentModel;
using Media_Player.Models;
using SimpleMediaPlayer;

namespace Media_Player.ViewModels
{
    class PlaylistViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<VideoFile> Videos { get; set; }

        public PlaylistViewModel()
        {
            var main = System.Windows.Application.Current.MainWindow as MainWindow;
            Videos = main?.LoadPlaylistVideos(1) ?? new ObservableCollection<VideoFile>(); //noch statisch auf 1 da keine anderen da sin
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
