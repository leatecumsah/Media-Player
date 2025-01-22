using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SimpleMediaPlayer
{
    public partial class MainWindow : Window
    {
        private string selectedFilePath = null;
        private string userMusicFolder;

        public MainWindow()
        {
            InitializeComponent();
            userMusicFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Videos";
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (mediaElement.Source != null)
                {
                    mediaElement.Play();
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
            }
            else
            {
                MessageBox.Show("Nichts wird abgespielt!", "Hinweis", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Select_Folder(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Datei auswählen",
                InitialDirectory = userMusicFolder,
                Filter = "Videodateien|*.mp4",
                Multiselect = false //true um mehrere dateien auszuwählen
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try{
                    selectedFilePath = openFileDialog.FileName;
                mediaElement.Source = new Uri(selectedFilePath);
                //abspielen
                mediaElement.LoadedBehavior = MediaState.Manual; // Manuelles Steuern erlauben
                mediaElement.UnloadedBehavior = MediaState.Stop; // Stop bei Entladen?? idk how to call that ask tim
                mediaElement.Play(); // Video starten }
                }
                    catch(Exception ex)
                    {
                    MessageBox.Show($"Fehler beim Laden der Datei: {ex.Message}", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                
                }
            }
        }

        private void BTN_Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BTN_Maximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState ==WindowState.Maximized )
                WindowState = WindowState.Normal;
            else
                WindowState = WindowState.Maximized;
        }

        private void BTN_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

