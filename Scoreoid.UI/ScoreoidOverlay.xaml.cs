using Scoreoid.UI.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Scoreoid.UI
{
    public sealed partial class ScoreoidOverlay : UserControl
    {
        public ScoreoidOverlay()
        {
            this.InitializeComponent();

            Loaded += ScoreoidOverlay_Loaded;            
        }

        public void Refresh()
        {
            username.Text = string.Empty;
            password.Password = string.Empty;

            if (string.IsNullOrEmpty(ScoreoidManager.username))
            {
                Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else
            {
                Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        void ScoreoidOverlay_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void CreatePlayer_Tapped(object sender, TappedRoutedEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(ScoreoidCreatePlayerPage));
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
                Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            else
            {
                MessageDialog dlg = new MessageDialog("Please enter a valid scoreoid username and/or password");
                dlg.Title = "ERROR";
                dlg.ShowAsync();
            }
        }
    }
}
