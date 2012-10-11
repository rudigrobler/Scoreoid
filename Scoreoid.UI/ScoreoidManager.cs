﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Scoreoid.UI
{
    public static class ScoreoidManager
    {
        private static string api_key = string.Empty;
        private static string game_id = string.Empty;

        public static void Initialize(string api_key, string game_id)
        {
            ScoreoidManager.api_key = api_key;
            ScoreoidManager.game_id = game_id;
        }

        public static event EventHandler Refresh;

        private static ScoreoidClient _scoreoidClient;
        internal static ScoreoidClient ScoreoidClient
        {
            get
            {
                if (string.IsNullOrEmpty(api_key) || string.IsNullOrEmpty(game_id))
                    throw new ScoreoidException("ScoreClient not initialized.");

                if (_scoreoidClient == null)
                    _scoreoidClient = new ScoreoidClient(api_key, game_id);

                return _scoreoidClient;
            }
        }

        public static string username
        {
            get
            {
                var container = ApplicationData.Current.LocalSettings.CreateContainer("scoreoid", ApplicationDataCreateDisposition.Always);
                if (container.Values.ContainsKey("username"))
                    return (string)container.Values["username"];
                return string.Empty;
            }
            set
            {
                var container = ApplicationData.Current.LocalSettings.CreateContainer("scoreoid", ApplicationDataCreateDisposition.Always);
                container.Values["username"] = value;

                if (Refresh != null)
                    Refresh(null, EventArgs.Empty);
            }
        }

        public static string password
        {
            get
            {
                var container = ApplicationData.Current.LocalSettings.CreateContainer("scoreoid", ApplicationDataCreateDisposition.Always);
                if (container.Values.ContainsKey("password"))
                    return (string)container.Values["password"];
                return string.Empty;
            }
            set
            {
                var container = ApplicationData.Current.LocalSettings.CreateContainer("scoreoid", ApplicationDataCreateDisposition.Always);
                container.Values["password"] = value;
            }
        }

        public static void ResetSettings()
        {
            ApplicationData.Current.LocalSettings.DeleteContainer("scoreoid");

            if (Refresh != null)
                Refresh(null, EventArgs.Empty);
        }

        public static async Task<string> CreateScore(int score)
        {
            var _score = await ScoreoidClient.CreateScore(username, score);

            if (Refresh != null)
                Refresh(null, EventArgs.Empty);

            return _score;
        }

        public static async Task<Leaderboard> GetLeaderboard(string order_by = "score", string order = "desc", int limit = 10)
        {
            Leaderboard leaderboard = new Leaderboard();
            var scores = await ScoreoidClient.GetBestScores(order_by, order, limit);
            int rank = 1;
            leaderboard.Items = (from _ in scores.items
                                    select new LeaderboardItem()
                                        {
                                            Rank = rank++,
                                            Player = _.username,
                                            Score = _.scores.First().value
                                        }).ToArray();

            return leaderboard;
        }
    }
}
