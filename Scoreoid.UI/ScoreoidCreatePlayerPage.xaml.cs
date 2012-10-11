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

namespace Scoreoid.UI.Primitives
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class ScoreoidCreatePlayerPage : Page
    {
        public ScoreoidCreatePlayerPage()
        {
            this.InitializeComponent();
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            player player = new player();

            if (!string.IsNullOrEmpty(email.Text))
            {
                player.email = email.Text;
            }
            if (!string.IsNullOrEmpty(password.Password))
            {
                player.password = password.Password;
            }
            if (!string.IsNullOrEmpty(firstName.Text))
            {
                player.first_name = firstName.Text;
            }
            if (!string.IsNullOrEmpty(lastName.Text))
            {
                player.last_name = lastName.Text;
            }            
            
            if (!string.IsNullOrEmpty(username.Text))
            {
                player.username = username.Text;

                try
                {
                    string response = await ScoreoidManager.ScoreoidClient.CreatePlayer(player);

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

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

    }
}
