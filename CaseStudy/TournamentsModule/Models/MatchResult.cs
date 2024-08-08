using Case.PlayersModule.Models;

namespace Case.TournamentsModule.Models
{
    public class MatchResult
    {
        public Player Winner { get; private set; }
        public Player Loser { get; private set; }

        public MatchResult(Player winner, Player loser)
        {
            Winner = winner;
            Loser = loser;
        }
    }
}
