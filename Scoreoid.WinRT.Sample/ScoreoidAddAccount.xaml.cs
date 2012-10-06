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

namespace Scoreoid.WinRT.Sample
{
    public sealed partial class ScoreoidAddAccount : UserControl
    {
        public ScoreoidAddAccount()
        {
            this.InitializeComponent();

            Loaded += ScoreoidAddAccount_Loaded;
        }

        void ScoreoidAddAccount_Loaded(object sender, RoutedEventArgs e)
        {
            if (App.ScoreoidSettings.ContainsKey("username"))
            {
                Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        private void CreatePlayer_Tapped(object sender, TappedRoutedEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(ScoreoidCreatePlayerPage));
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            rootLayout.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            App.ScoreoidSettings["username"] = username.Text;
            App.ScoreoidSettings["password"] = password.Text;

            rootLayout.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
    }
}
