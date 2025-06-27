using Media_Player.Data;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using TagLib;//nugget für Metadaten und so
using Media_Player.ViewModels;
using Media_Player.Models;
using System.IO;
using MySql.Data.MySqlClient; // MySQL Connector für C#
using System.Collections.ObjectModel; // Für ObservableCollection



namespace SimpleMediaPlayer
{
    public partial class MainWindow : Window
    {
        private string selectedFilePath = null;
        private readonly string videoFolder = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
        @"Videos\Mediaplayer-Samples");

        private string userMusicFolder;
        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
            userMusicFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Videos";
            /*DbConnectionTest.TestConnection();*/ /*Muss unkommentiert sein um verbindung zur db zu testen*/
            DataContext = new PlaylistViewModel();
            var videos = LoadPlaylistVideos(1); // Playlist mit ID 1
            myListBox.ItemsSource = videos;

            // Fortschrittsanzeige aktualisieren
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(0.05)
            };
            timer.Tick += Timer_Tick;
             mediaElement.Volume = 0.5;//lautstärke default 5ß%
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (mediaElement.Source != null && mediaElement.NaturalDuration.HasTimeSpan)
            {
                ProgressSlider.Maximum = mediaElement.NaturalDuration.TimeSpan.TotalSeconds;
                ProgressSlider.Value = mediaElement.Position.TotalSeconds;
            }
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (mediaElement.Source != null)
                {
                    mediaElement.Play();
                    timer.Start();
                }
                else
                {
                    MessageBox.Show("Bitte zuerst Datei auswählen!", "Keine Datei ausgewählt", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Abspielen der Datei: {ex.Message}", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (mediaElement.Source != null)
            {
                mediaElement.Pause();
            }
            else
            {
                MessageBox.Show("Nichts wird abgespielt!", "Hinweis", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            if (mediaElement.Source != null)
            {
                mediaElement.Stop();
                timer.Stop();
                ProgressSlider.Value = 0;
            }
            else
            {
                MessageBox.Show("Nichts wird abgespielt!", "Hinweis", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (mediaElement != null)
            {
                mediaElement.Volume = VolumeSlider.Value;
            }
        }


        private void Select_Folder(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Datei auswählen",
                InitialDirectory = userMusicFolder,
                Filter = "Videodateien|*.mp4",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    selectedFilePath = openFileDialog.FileName;
                    mediaElement.Source = new Uri(selectedFilePath);

                    mediaElement.MinHeight = 50;
                    mediaElement.MinWidth = 50;

                    // Video automatisch abspielen
                    mediaElement.LoadedBehavior = MediaState.Manual;
                    mediaElement.UnloadedBehavior = MediaState.Stop;
                    mediaElement.Play();
                    timer.Start();

                    // Metadaten abrufen
                    TagLib.File file = TagLib.File.Create(selectedFilePath);
                    MetaTitle.Text = file.Tag.Title ?? "Unbekannt";
                    MetaDuration.Text = file.Properties.Duration.ToString(@"hh\:mm\:ss");
                    MetaBitrate.Text = file.Properties.AudioBitrate.ToString() + " kbps";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fehler beim Laden der Datei: {ex.Message}", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ProgressSlider_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (mediaElement.Source != null)
            {
                mediaElement.Position = TimeSpan.FromSeconds(ProgressSlider.Value);
            }
        }

        private void BTN_Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BTN_Maximize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = (WindowState == WindowState.Maximized) ? WindowState.Normal : WindowState.Maximized;
        }

        private void BTN_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            // Nächster Track (optional)
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            // Vorheriger Track (optional)
        }

        private void myListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (myListBox.SelectedItem is VideoFile selectedVideo)
            {
                try
                {
                    string fullPath = Path.Combine(videoFolder, selectedVideo.FilePath);

                    // Prüfen ob Datei existiert
                    if (!System.IO.File.Exists(fullPath))
                    {
                        MessageBox.Show("Video nicht gefunden:\n" + fullPath, "Fehler", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }



                    mediaElement.Stop();
                    mediaElement.Source = new Uri(fullPath, UriKind.Absolute);
                     // Sicherheits-Reset
                    mediaElement.LoadedBehavior = MediaState.Manual;
                    mediaElement.UnloadedBehavior = MediaState.Stop;
                    mediaElement.Play();
                    
                    MetaTitle.Text = selectedVideo.Title;
                    MetaDuration.Text = selectedVideo.Duration.ToString(@"hh\:mm\:ss");
                    MetaBitrate.Text = "N/A"; // Falls Bitrate nicht geladen wird

                    timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(0.05)
            };
            timer.Tick += Timer_Tick;
             mediaElement.Volume = 0.5;//lautstärke default 5ß%

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fehler beim Abspielen: " + ex.Message);
                }
            }
        }


        private void PlayVideo(VideoFile videoFile)
        {
            string musicFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            string fullPath = Path.Combine(musicFolder, videoFile.FilePath);

            mediaElement.Source = new Uri(fullPath);
            mediaElement.Play();
        }

       
        public ObservableCollection<VideoFile> LoadPlaylistVideos(int playlistID)
        {
            var videos = new ObservableCollection<VideoFile>();
            string connectionString = "Server=localhost;Database=mediplayer_db;Uid=root;Pwd=;"; // MySQL-Verbindung
            
            string query = @"
        SELECT vf.id, vf.title, vf.filePath, vf.duration
        FROM Playlist_Video pv
        JOIN VideoFile vf ON pv.VideoFileID = vf.id
        WHERE pv.PlaylistID = @playlistID";

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@playlistID", playlistID);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            videos.Add(new VideoFile
                            {
                                Id = reader.GetInt32("id"),
                                Title = reader.GetString("title"),
                                FilePath = reader.GetString("filePath"),
                                Duration = reader.GetTimeSpan("duration")
                            });
                        }
                    }
                }
            }

            return videos;
        }

    }
}
