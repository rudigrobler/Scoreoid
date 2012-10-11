using Scoreoid.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Scoreoid.Sample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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

        private void ResetSettings_Click(object sender, RoutedEventArgs e)
        {
            ScoreoidManager.ResetSettings();
            scoreoidOverlay.Refresh();
        }

        private async void GetLeaderboard_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                getLEaderboard.IsEnabled = false;

                var leaderboard = await ScoreoidManager.GetLeaderboard();
                string leaderboardDebugString = string.Empty;
                foreach (var item in leaderboard.Items)
                {
                    leaderboardDebugString += item.Rank + " - " + item.Player + " - " + item.Score + Environment.NewLine;
                }

                MessageDialog dlg = new MessageDialog(leaderboardDebugString);
                dlg.Title = "LEADERBOARD";
                dlg.ShowAsync();

                getLEaderboard.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageDialog dlg = new MessageDialog(ex.Message);
                dlg.Title = "ERROR";
                dlg.ShowAsync();

                getLEaderboard.IsEnabled = true;
            }
            getLEaderboard.IsEnabled = true;
        }

        private async void CreateScore_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                createScore.IsEnabled = false;
                score.IsEnabled = false;

                int _score = int.Parse(score.Text);
                var response = await ScoreoidManager.CreateScore(_score);

                MessageDialog dlg = new MessageDialog(response);
                dlg.Title = "SCORE";
                dlg.ShowAsync();

                createScore.IsEnabled = true;
                score.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageDialog dlg = new MessageDialog(ex.Message);
                dlg.Title = "ERROR";
                dlg.ShowAsync();

                createScore.IsEnabled = true;
                score.IsEnabled = true;
            }
            createScore.IsEnabled = true;
            score.IsEnabled = true;
        }
    }
}
