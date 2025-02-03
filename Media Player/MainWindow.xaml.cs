using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using TagLib;//nugget für Metadaten und so


namespace SimpleMediaPlayer
{
    public partial class MainWindow : Window
    {
        private string selectedFilePath = null;
        private string userMusicFolder;
        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
            userMusicFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Videos";

            // Fortschrittsanzeige aktualisieren
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += Timer_Tick;
             mediaElement.Volume = 0.5;//lautstärke 5ß%
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
    }
}
