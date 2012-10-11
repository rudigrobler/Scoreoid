using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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
    public sealed partial class GameCard : UserControl
    {
        public GameCard()
        {
            this.InitializeComponent();
            Loaded += GameCard_Loaded;
            Unloaded += GameCard_Unloaded;
        }

        void GameCard_Unloaded(object sender, RoutedEventArgs e)
        {
            ScoreoidManager.Refresh -= ScoreoidManager_Refresh;
        }

        void GameCard_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();

            ScoreoidManager.Refresh += ScoreoidManager_Refresh;
        }

        void ScoreoidManager_Refresh(object sender, EventArgs e)
        {
            Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, new Windows.UI.Core.DispatchedHandler(
                () => 
                {
                    Refresh();
                }));            
        }

        public async void Refresh()
        {
            inProgress.Visibility = Windows.UI.Xaml.Visibility.Visible;
            errorDetails.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            playerDetails.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            if (!string.IsNullOrEmpty(ScoreoidManager.username))
            {
                try
                {
                    var player = await ScoreoidManager.ScoreoidClient.GetPlayer(ScoreoidManager.username, ScoreoidManager.password);

                    inProgress.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    errorDetails.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    playerDetails.Visibility = Windows.UI.Xaml.Visibility.Visible;

                    username.Text = player.items.First().username;
                    best_score.Text = player.items.First().best_score;
                }
                catch (ScoreoidException ex)
                {
                    inProgress.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    errorDetails.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    playerDetails.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    errorMessage.Text = ex.Message;
                }
                catch (HttpRequestException ex)
                {
                    inProgress.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    errorDetails.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    playerDetails.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    errorMessage.Text = ex.Message;
                }
            }
            else
            {
                inProgress.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                errorDetails.Visibility = Windows.UI.Xaml.Visibility.Visible;
                playerDetails.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            
                errorMessage.Text = "No username/password";
            }
        }

        private void Refresh_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (inProgress.Visibility != Windows.UI.Xaml.Visibility.Visible)
            {
                Refresh();
            }
        }
    }
}
