using System;
using System.Windows;

namespace SimpleMediaPlayer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    

    private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            // Pfad zur Mediendatei
            string mediaPath = "C:\\Users\\leabu\\Downloads\\Pariah Nexus - S01E02 - Doctrine and Discipline WEBDL-1080p.mkv"; // Ändere dies auf einen Dateipfad!!!!!
            mediaElement.Source = new Uri(mediaPath);
            mediaElement.Play();
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Pause();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
        }

    } 
}

