using Scoreoid.UI.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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

        ApplicationDataContainer container;

        void ScoreoidOverlay_Loaded(object sender, RoutedEventArgs e)
        {
            container = ApplicationData.Current.LocalSettings.CreateContainer("scoreoid", ApplicationDataCreateDisposition.Always);
            if (container.Values.ContainsKey("username"))
            {
                Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            rootLayout.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void CreatePlayer_Tapped(object sender, TappedRoutedEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(ScoreoidCreatePlayerPage));
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            container.Values["username"] = username.Text;
            container.Values["password"] = password.Password;

            rootLayout.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
    }
}
