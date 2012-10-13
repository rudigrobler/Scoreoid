using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ScoreoidUI
{
    public class Leaderboard : ObservableCollection<LeaderboardItem>
    {
        public Leaderboard()
        {
        }

        public Leaderboard(IEnumerable<LeaderboardItem> collection)
            : base(collection)
        {
        }
    }
}