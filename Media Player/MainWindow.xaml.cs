using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media;

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

            // Zugriff auf die GradientStops
            var gradientBrush = (LinearGradientBrush)MainBorderBrush;
            var gradientStop1 = gradientBrush.GradientStops[0];
            var gradientStop2 = gradientBrush.GradientStops[1];
            var gradientStop3 = gradientBrush.GradientStops[2];

            // NameScope erstellen und GradientStops registrieren
            NameScope.SetNameScope(this, new NameScope());
            this.RegisterName("GradientStop1", gradientStop1);
            this.RegisterName("GradientStop2", gradientStop2);
            this.RegisterName("GradientStop3", gradientStop3);

            // Animationen erstellen
            var colorAnimation1 = new ColorAnimation
            {
                From = Colors.Purple, // #B47EB3
                To = Colors.Pink,    // #FFD5FF
                Duration = new Duration(TimeSpan.FromSeconds(5)),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };

            var colorAnimation2 = new ColorAnimation
            {
                From = Colors.Pink,  // #FFD5FF
                To = Colors.LightGreen, // #8BB8A8
                Duration = new Duration(TimeSpan.FromSeconds(5)),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };

            var colorAnimation3 = new ColorAnimation
            {
                From = Colors.LightGreen, // #8BB8A8
                To = Colors.Purple,       // #B47EB3
                Duration = new Duration(TimeSpan.FromSeconds(5)),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };

            // Animationen den GradientStops zuweisen
            Storyboard.SetTargetName(colorAnimation1, "GradientStop1");
            Storyboard.SetTargetProperty(colorAnimation1, new PropertyPath(GradientStop.ColorProperty));
            Storyboard.SetTargetName(colorAnimation2, "GradientStop2");
            Storyboard.SetTargetProperty(colorAnimation2, new PropertyPath(GradientStop.ColorProperty));
            Storyboard.SetTargetName(colorAnimation3, "GradientStop3");
            Storyboard.SetTargetProperty(colorAnimation3, new PropertyPath(GradientStop.ColorProperty));

            // Storyboard erstellen und starten
            var storyboard = new Storyboard();
            storyboard.Children.Add(colorAnimation1);
            storyboard.Children.Add(colorAnimation2);
            storyboard.Children.Add(colorAnimation3);
            storyboard.Begin(this);
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

        private void ChangeMediaVolume(object sender, RoutedPropertyChangedEventArgs<double> args)
      {
            mediaElement.Volume = (double)volumeSlider.Value;
      }

        private void Next_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

