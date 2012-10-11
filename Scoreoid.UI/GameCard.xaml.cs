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
        }

        void GameCard_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateGameCard();
        }

        private async void UpdateGameCard()
        {
            var container = ApplicationData.Current.LocalSettings.CreateContainer("scoreoid", ApplicationDataCreateDisposition.Always);

            string _username = string.Empty;
            if (container.Values.ContainsKey("username"))
            {
                _username = container.Values["username"].ToString();
            }

            string _password = string.Empty;
            if (container.Values.ContainsKey("password"))
            {
                _password = container.Values["password"].ToString();
            }

            if (!string.IsNullOrEmpty(_username))
            {
                try
                {
                    var player = await ScoreoidManager.ScoreoidClient.GetPlayer(_username, _password);

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
    }
}
