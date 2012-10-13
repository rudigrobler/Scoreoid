using System;
using ScoreoidUI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Scoreoid.Sample
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
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

                Leaderboard leaderboard = await ScoreoidManager.GetLeaderboard();
                string leaderboardDebugString = string.Empty;
                foreach (LeaderboardItem item in leaderboard)
                {
                    leaderboardDebugString += item.Rank + " - " + item.Player + " - " + item.Score + Environment.NewLine;
                }

                var dlg = new MessageDialog(leaderboardDebugString) {Title = "LEADERBOARD"};
                dlg.ShowAsync();

                getLEaderboard.IsEnabled = true;
            }
            catch (Exception ex)
            {
                var dlg = new MessageDialog(ex.Message) {Title = "ERROR"};
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
                string response = await ScoreoidManager.CreateScore(_score);

                var dlg = new MessageDialog(response) {Title = "SCORE"};
                dlg.ShowAsync();

                createScore.IsEnabled = true;
                score.IsEnabled = true;
            }
            catch (Exception ex)
            {
                var dlg = new MessageDialog(ex.Message) {Title = "ERROR"};
                dlg.ShowAsync();

                createScore.IsEnabled = true;
                score.IsEnabled = true;
            }
            createScore.IsEnabled = true;
            score.IsEnabled = true;
        }
    }
}