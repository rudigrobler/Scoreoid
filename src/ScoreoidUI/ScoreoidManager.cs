using System;
using System.Linq;
using System.Threading.Tasks;
using Scoreoid;
using Windows.Storage;

namespace ScoreoidUI
{
    public static class ScoreoidManager
    {
        private static string api_key;
        private static string game_id;

        private static ScoreoidClient _scoreoidClient;

        static ScoreoidManager()
        {
            CachedPlayer = null;
        }

        internal static ScoreoidClient ScoreoidClient
        {
            get
            {
                if (string.IsNullOrEmpty(api_key) || string.IsNullOrEmpty(game_id))
                    throw new ScoreoidException("ScoreClient not initialized.");

                return _scoreoidClient ?? (_scoreoidClient = new ScoreoidClient(api_key, game_id));
            }
        }

        public static player CachedPlayer { get; set; }

        public static string username
        {
            get
            {
                ApplicationDataContainer container = ApplicationData.Current.LocalSettings.CreateContainer("scoreoid",
                                                                                                           ApplicationDataCreateDisposition
                                                                                                               .Always);
                if (container.Values.ContainsKey("username"))
                    return (string) container.Values["username"];
                return string.Empty;
            }
            set
            {
                ApplicationDataContainer container = ApplicationData.Current.LocalSettings.CreateContainer("scoreoid",
                                                                                                           ApplicationDataCreateDisposition
                                                                                                               .Always);
                container.Values["username"] = value;

                if (Refresh != null)
                    Refresh(null, EventArgs.Empty);
            }
        }

        public static string password
        {
            get
            {
                ApplicationDataContainer container = ApplicationData.Current.LocalSettings.CreateContainer("scoreoid",
                                                                                                           ApplicationDataCreateDisposition
                                                                                                               .Always);
                if (container.Values.ContainsKey("password"))
                    return (string) container.Values["password"];
                return string.Empty;
            }
            set
            {
                ApplicationDataContainer container = ApplicationData.Current.LocalSettings.CreateContainer("scoreoid",
                                                                                                           ApplicationDataCreateDisposition
                                                                                                               .Always);
                container.Values["password"] = value;
            }
        }

        public static void Initialize(string _api_key, string _game_id)
        {
            api_key = _api_key;
            game_id = _game_id;
        }

        public static event EventHandler Refresh;

        public static void ResetSettings()
        {
            ApplicationData.Current.LocalSettings.DeleteContainer("scoreoid");

            if (Refresh != null)
                Refresh(null, EventArgs.Empty);
        }

        public static async Task<string> CreateScore(int score)
        {
            string _score = await ScoreoidClient.CreateScoreAsync(username, score);

            if (Refresh != null)
                Refresh(null, EventArgs.Empty);

            return _score;
        }


        public static async Task<Leaderboard> GetLeaderboard()
        {
            return await GetLeaderboard("score", "desc", 10);
        }

        public static async Task<Leaderboard> GetLeaderboard(string order_by, string order, int limit)
        {
            scores scores = await ScoreoidClient.GetBestScoresAsync(order_by, order, limit);
            int rank = 1;

            return new Leaderboard(from _ in scores.items
                                   select new LeaderboardItem
                                              {
                                                  Rank = rank++,
                                                  Player = _.username,
                                                  Score = _.scores.First().value
                                              });
        }
    }
}