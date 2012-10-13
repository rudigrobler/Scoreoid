using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace ScoreoidUI
{
    public sealed partial class ScoreoidPane : UserControl
    {
        public ScoreoidPane()
        {
            this.InitializeComponent();
            Loaded += ScoreoidPane_Loaded;
        }

        async void ScoreoidPane_Loaded(object sender, RoutedEventArgs e)
        {
            var games = await ScoreoidManager.ScoreoidClient.GetGameAsync();
            var game = games.items.First();
            gameName.Text = game.name;
            gameDescription.Text = game.short_description;
            gameVersion.Text = game.version;
            gameWebsite.NavigateUri = new Uri(game.website_url);

            var players =
                await ScoreoidManager.ScoreoidClient.GetPlayerAsync(ScoreoidManager.username, ScoreoidManager.password);
            var player = players.items.First();

            username.Text = player.username;
            firstName.Text = player.first_name;
            lastName.Text = player.last_name;
            email.Text = player.email;
        }
    }
}
