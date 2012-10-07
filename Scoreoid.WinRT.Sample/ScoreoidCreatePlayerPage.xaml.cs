using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Scoreoid.WinRT.Sample
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class ScoreoidCreatePlayerPage : Scoreoid.WinRT.Sample.Common.LayoutAwarePage
    {
        public ScoreoidCreatePlayerPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            player player = new player();

            if (!string.IsNullOrEmpty(email.Text))
            {
                player.email = email.Text;
            }
            if (!string.IsNullOrEmpty(password.Text))
            {
                player.password = password.Text;
            }
            if (!string.IsNullOrEmpty(username.Text))
            {
                player.username = username.Text;

                try
                {
                    string response = await App.ScoreoidClient.CreatePlayer(player);

                    MessageDialog dlg = new MessageDialog(response);
                    await dlg.ShowAsync();

                    this.Frame.GoBack();
                }
                catch (ScoreoidException ex)
                {
                    MessageDialog dlg = new MessageDialog(ex.Message);
                    dlg.Title = "ERROR";
                    dlg.ShowAsync();
                }
                catch (HttpRequestException ex)
                {
                    var dlg = new MessageDialog(ex.Message);
                    dlg.Title = "ERROR";
                    dlg.ShowAsync();
                }
            }
            else
            {
                MessageDialog dlg = new MessageDialog("username is required");
                dlg.Title = "ERROR";
                dlg.ShowAsync();
            }
        }

    }
}
