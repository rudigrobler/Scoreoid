using Scoreoid.UI.Primitives;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Scoreoid.UI
{
    public sealed partial class ScoreoidOverlay : UserControl
    {
        public ScoreoidOverlay()
        {
            InitializeComponent();

            Loaded += ScoreoidOverlay_Loaded;
        }

        public void Refresh()
        {
            username.Text = string.Empty;
            password.Password = string.Empty;

            if (string.IsNullOrEmpty(ScoreoidManager.username))
            {
                Visibility = Visibility.Visible;
            }
            else
            {
                Visibility = Visibility.Collapsed;
            }
        }

        private void ScoreoidOverlay_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }

        private void CreatePlayer_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var frame = Window.Current.Content as Frame;
            if (frame != null)
                frame.Navigate(typeof (ScoreoidCreatePlayerPage));
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(username.Text))
            {
                ScoreoidManager.username = username.Text;
                if (!string.IsNullOrEmpty(password.Password))
                {
                    ScoreoidManager.password = password.Password;
                }
                Visibility = Visibility.Collapsed;
            }
            else
            {
                var dlg = new MessageDialog("Please enter a valid scoreoid username and/or password") {Title = "ERROR"};
                dlg.ShowAsync();
            }
        }

        private void password_KeyDown_1(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                SaveButton_Click(sender, new RoutedEventArgs());
            }
        }
    }
}