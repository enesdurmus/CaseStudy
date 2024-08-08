using Case.PlayersModule.Models;
using Case.TournamentsModule.Models;

namespace Case.TournamentsModule.Interfaces
{
    public interface ITournamentStrategy
    {
        public void MatchPlayers(Tournament tournament, Player[] contenders, IMatchRuleStrategy matchRuleStrategy);
    }
}
