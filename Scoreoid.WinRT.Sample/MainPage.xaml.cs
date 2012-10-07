using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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

namespace Scoreoid.WinRT.Sample
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string username = string.Empty;
            if (App.ScoreoidSettings.ContainsKey("username"))
            {
                username = App.ScoreoidSettings["username"].ToString();
            }

            string password = string.Empty;
            if (App.ScoreoidSettings.ContainsKey("password"))
            {
                password = App.ScoreoidSettings["password"].ToString();
            }

            if (!string.IsNullOrEmpty(username))
            {
                try
                {
                    var player = await App.ScoreoidClient.GetPlayer(username, password);

                    var dlg = new MessageDialog("Score: " + player.items.First().best_score);
                    dlg.Title = player.items.First().username;
                    dlg.ShowAsync();
                }
                catch (ScoreoidException ex)
                {
                    var dlg = new MessageDialog(ex.Message);
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
                var dlg = new MessageDialog("No username and/or password");
                dlg.Title = "ERROR";
                dlg.ShowAsync();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            App.ResetScoreoidSettings();
        }

        //private async void Button_Click_3(object sender, RoutedEventArgs e)
        //{
        //    if (App.ScoreoidSettings.ContainsKey("username"))
        //    {
        //        try
        //        {
        //            int _score = int.Parse(score.Text);
        //            var response = await App.ScoreoidClient.CreateScore(App.ScoreoidSettings["username"].ToString(), _score);

        //            var dlg = new MessageDialog(response);
        //            dlg.ShowAsync();
        //        }
        //        catch (ScoreoidException ex)
        //        {
        //            var dlg = new MessageDialog(ex.Message);
        //            dlg.Title = "ERROR";
        //            dlg.ShowAsync();
        //        }
        //        catch (HttpRequestException ex)
        //        {
        //            var dlg = new MessageDialog(ex.Message);
        //            dlg.Title = "ERROR";
        //            dlg.ShowAsync();
        //        }
        //    }
        //    else
        //    {
        //        var dlg = new MessageDialog("No username and/or password");
        //        dlg.Title = "ERROR";
        //        dlg.ShowAsync();
        //    }
        //}

        private async void Button_Click_4(object sender, RoutedEventArgs e)
        {
            try
            {
                var scores = await App.ScoreoidClient.GetBestScores();

                string leaderboard = string.Empty;
                int index = 1;
                foreach (var player in scores.items)
                {
                    leaderboard += index;
                    leaderboard += " - ";
                    leaderboard += player.username;
                    leaderboard += " - ";
                    leaderboard += player.scores.First().value;
                    leaderboard += Environment.NewLine;

                    index++;
                }

                var dlg = new MessageDialog(leaderboard);
                dlg.ShowAsync();
            }
            catch (ScoreoidException ex)
            {
                var dlg = new MessageDialog(ex.Message);
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
    }
}
