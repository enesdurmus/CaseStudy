using Case.PlayersModule.Models;
using Case.TournamentsModule.Factory;
using Case.TournamentsModule.Interfaces;
using Case.TournamentsModule.Models;

namespace Case.TournamentsModule.Services
{
    public class TournamentService
    {
        private readonly TournamentStrategyFactory tournamentStrategyFactory;
        private readonly MatchRulesStrategyFactory matchRulesStrategyFactory;

        public TournamentService(TournamentStrategyFactory tournamentStrategyFactory, MatchRulesStrategyFactory matchRulesStrategyFactory)
        {
            this.tournamentStrategyFactory = tournamentStrategyFactory;
            this.matchRulesStrategyFactory = matchRulesStrategyFactory;
        }

        public void StartTournament(Tournament tournament, Player[] contenders)
        {
            IMatchRuleStrategy matchRuleStrategy = matchRulesStrategyFactory.CreateMatchRuleStrategy(tournament.MatchRule);
            ITournamentStrategy tournamentStrategy = tournamentStrategyFactory.CreateTournamentStrategy(tournament.TournamentStrategy);
            tournamentStrategy.MatchPlayers(tournament, contenders, matchRuleStrategy);
        }
    }
}
