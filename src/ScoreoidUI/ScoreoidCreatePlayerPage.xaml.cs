using System;
using System.Net.Http;
using Scoreoid;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ScoreoidUI
{
    public sealed partial class ScoreoidCreatePlayerPage : Page
    {
        public ScoreoidCreatePlayerPage()
        {
            InitializeComponent();
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            save.IsEnabled = false;

            var player = new player();

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
                    string response = await ScoreoidManager.ScoreoidClient.CreatePlayerAsync(player);

                    var dlg = new MessageDialog(response) {Title = "PLAYER"};
                    await dlg.ShowAsync();

                    Frame.GoBack();

                    save.IsEnabled = true;
                }
                catch (ScoreoidException ex)
                {
                    var dlg = new MessageDialog(ex.Message) {Title = "ERROR"};
                    dlg.ShowAsync();

                    save.IsEnabled = true;
                }
                catch (HttpRequestException ex)
                {
                    var dlg = new MessageDialog(ex.Message) {Title = "ERROR"};
                    dlg.ShowAsync();

                    save.IsEnabled = true;
                }
            }
            else
            {
                var dlg = new MessageDialog("username is required") {Title = "ERROR"};
                await dlg.ShowAsync();

                save.IsEnabled = true;
            }

            save.IsEnabled = true;
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}