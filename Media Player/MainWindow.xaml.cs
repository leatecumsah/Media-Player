using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using MySqlConnector;
namespace SimpleMediaPlayer
{
    public partial class MainWindow : Window
    {
        private string selectedFilePath = null;
        private string userMusicFolder ="";
        public MainWindow()
        {
            InitializeComponent();
            userMusicFolder = System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            
            
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
                MessageBox.Show($"Fehler: {ex.Message}", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            if(mediaElement.Source != null)
                {
                mediaElement.Pause();
                }
            else { 
                MessageBox.Show("Nichts wird abgespeilt","Hinweis",MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            if(mediaElement.Source != null)
                {
                mediaElement.Stop();
                }
            else { 
                MessageBox.Show("Nichts wird abgespeilt","Hinweis",MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            
        }

        private void Choose_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Select_Folder(object sender, RoutedEventArgs e)
            { 

            
          OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Datei auswählen";
            openFileDialog.InitialDirectory = userMusicFolder+ "\\Music";
            openFileDialog.Filter ="Videofiles|*.mp4;";
            openFileDialog.Multiselect = true;
            
            if(openFileDialog.ShowDialog() == true)
                
              { 
                
                selectedFilePath = openFileDialog.FileName;
                mediaElement.Source = new Uri(selectedFilePath);
                MessageBox.Show($"Ausgewählte Datei: {selectedFilePath}");
                mediaElement.Play();
                TestPlayBack();
                
                }
            }
        private void TestPlayBack()
            {
            string testFilePath=@"C:\Users\buerklele\Music\Shrimp.mp4";
            mediaElement.Source = new Uri(testFilePath);
            mediaElement.Play();
            }
        private void ChangeMediaVolume(object sender, RoutedEventArgs e)
            {
            mediaElement.Volume = (int)volumeSlider.Value;
            }

        private void BTN_Minimize_Click(object sender, RoutedEventArgs e)
        {

        }
    } 
}

