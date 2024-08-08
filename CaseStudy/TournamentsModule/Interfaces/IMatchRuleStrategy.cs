using Case.PlayersModule.Models;
using Case.TournamentsModule.Enums;
using Case.TournamentsModule.Models;

namespace Case.TournamentsModule.Interfaces
{
    public interface IMatchRuleStrategy
    {
        public MatchResult PlayMatch(Player player1, Player player2, SurfaceType surfaceType);
    }
}
