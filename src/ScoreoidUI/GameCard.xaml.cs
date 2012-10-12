using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Scoreoid;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;

namespace ScoreoidUI
{
    public sealed partial class GameCard : UserControl
    {
        public GameCard()
        {
            InitializeComponent();
            Loaded += GameCard_Loaded;
            Unloaded += GameCard_Unloaded;
        }

        private void GameCard_Unloaded(object sender, RoutedEventArgs e)
        {
            ScoreoidManager.Refresh -= ScoreoidManager_Refresh;
        }

        private void GameCard_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();

            ScoreoidManager.Refresh += ScoreoidManager_Refresh;
        }

        private void ScoreoidManager_Refresh(object sender, EventArgs e)
        {
            ScoreoidManager.CachedPlayer = null;
            Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => { Refresh(); });
        }

        public async void Refresh()
        {
            inProgress.Visibility = Visibility.Visible;
            errorDetails.Visibility = Visibility.Collapsed;
            playerDetails.Visibility = Visibility.Collapsed;

            if (!string.IsNullOrEmpty(ScoreoidManager.username))
            {
                try
                {
                    if (ScoreoidManager.CachedPlayer==null)
                    {
                        var _players = await
                            ScoreoidManager.ScoreoidClient.GetPlayerAsync(ScoreoidManager.username, ScoreoidManager.password);
                        ScoreoidManager.CachedPlayer = _players.items.First();
                    }

                    inProgress.Visibility = Visibility.Collapsed;
                    errorDetails.Visibility = Visibility.Collapsed;
                    playerDetails.Visibility = Visibility.Visible;

                    username.Text = ScoreoidManager.CachedPlayer.username;
                    best_score.Text = ScoreoidManager.CachedPlayer.best_score;

                    if (!string.IsNullOrEmpty(ScoreoidManager.CachedPlayer.email))
                    {
                        var gravatarImage = new BitmapImage(GetGravatarUri(ScoreoidManager.CachedPlayer.email, 50));
                        gravatar.Source = gravatarImage;
                    }
                }
                catch (ScoreoidException ex)
                {
                    inProgress.Visibility = Visibility.Collapsed;
                    errorDetails.Visibility = Visibility.Visible;
                    playerDetails.Visibility = Visibility.Collapsed;
                    errorMessage.Text = ex.Message;
                }
                catch (HttpRequestException ex)
                {
                    inProgress.Visibility = Visibility.Collapsed;
                    errorDetails.Visibility = Visibility.Visible;
                    playerDetails.Visibility = Visibility.Collapsed;
                    errorMessage.Text = ex.Message;
                }
            }
            else
            {
                inProgress.Visibility = Visibility.Collapsed;
                errorDetails.Visibility = Visibility.Visible;
                playerDetails.Visibility = Visibility.Collapsed;

                errorMessage.Text = "No username/password";
            }
        }

        private void Refresh_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (inProgress.Visibility != Visibility.Visible)
            {
                Refresh();
            }
        }

        private Uri GetGravatarUri(string email, int width)
        {
            if (string.IsNullOrEmpty(email))
                return null;

            // Reference: http://en.gravatar.com/site/implement/url
            var sb = new StringBuilder();

            sb.Append("http://www.gravatar.com/avatar/");
            sb.Append(Md5EncodeText(email));
            sb.Append(".jpg");

            sb.Append("?s=");
            sb.Append(width);

            return new Uri(sb.ToString());
        }

        private string Md5EncodeText(string text)
        {
            IBuffer buffer = CryptographicBuffer.ConvertStringToBinary(text, BinaryStringEncoding.Utf8);
            HashAlgorithmProvider algorithmProvider = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);

            IBuffer hashedBuffer = algorithmProvider.HashData(buffer);
            return CryptographicBuffer.EncodeToHexString(hashedBuffer);
        }
    }
}