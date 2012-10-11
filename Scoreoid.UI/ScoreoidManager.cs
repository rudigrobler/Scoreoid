using System;
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

        private static ScoreoidClient scoreoidClient;
        public static ScoreoidClient ScoreoidClient
        {
            get
            {
                if (string.IsNullOrEmpty(api_key) || string.IsNullOrEmpty(game_id))
                    throw new ScoreoidException("ScoreClient not initialized.");

                if (scoreoidClient == null)
                    scoreoidClient = new ScoreoidClient(api_key, game_id);

                return scoreoidClient;
            }
        }

        public static void ResetSettings()
        {
            ApplicationData.Current.LocalSettings.DeleteContainer("scoreoid");
        }

    }
}
