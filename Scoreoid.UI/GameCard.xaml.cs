using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
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
                    var player = await ScoreoidManager.ScoreoidClient.GetPlayerAsync(ScoreoidManager.username, ScoreoidManager.password);

                    inProgress.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    errorDetails.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    playerDetails.Visibility = Windows.UI.Xaml.Visibility.Visible;

                    username.Text = player.items.First().username;
                    best_score.Text = player.items.First().best_score;

                    if (!string.IsNullOrEmpty(player.items.First().email))
                    {
                        var gravatarImage = new BitmapImage(GetGravatarUri(player.items.First().email, 50));
                        gravatar.Source = gravatarImage;
                    }
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

        protected Uri GetGravatarUri(string email, int width)
        {
            if (string.IsNullOrEmpty(email))
                return null;

            // Reference: http://en.gravatar.com/site/implement/url
            StringBuilder sb = new StringBuilder();

            sb.Append("http://www.gravatar.com/avatar/");
            sb.Append(Md5EncodeText(email));
            sb.Append(".jpg");

            sb.Append("?s=");
            sb.Append(width);

            return new Uri(sb.ToString());
        }

        protected string Md5EncodeText(string text)
        {
            IBuffer buffer = CryptographicBuffer.ConvertStringToBinary(text, BinaryStringEncoding.Utf8);
            HashAlgorithmProvider algorithmProvider = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);

            IBuffer hashedBuffer = algorithmProvider.HashData(buffer);
            return CryptographicBuffer.EncodeToHexString(hashedBuffer);
        }
    }
}
