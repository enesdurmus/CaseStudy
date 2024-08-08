using Case.PlayersModule.Models;
using Case.TournamentsModule.Interfaces;
using Case.TournamentsModule.Models;

namespace Case.TournamentsModule.Strategies.TournamentStrategies
{
    public class EliminationTournamentStrategy : ITournamentStrategy
    {
        private const int MATCH_WINNER_EXPERIENCE = 20;
        private const int MATCH_LOSER_EXPERIENCE = 10;

        private readonly IExperiencePointsService experiencePointsService;

        public EliminationTournamentStrategy(IExperiencePointsService experiencePointsService)
        {
            this.experiencePointsService = experiencePointsService;
        }

        public void MatchPlayers(Tournament tournament, Player[] contenders, IMatchRuleStrategy matchRuleStrategy)
        {
            List<Player> remainingPlayers = new List<Player>(contenders);

            while (remainingPlayers.Count > 1)
            {
                remainingPlayers = PlayRound(tournament, remainingPlayers, matchRuleStrategy);
            }

            LogTournamentInfo(tournament, remainingPlayers[0]);
        }

        private List<Player> PlayRound(Tournament tournament, List<Player> players, IMatchRuleStrategy matchRuleStrategy)
        {
            List<Player> nextRoundPlayers = new List<Player>();

            if (players.Count % 2 != 0)
            {
                nextRoundPlayers.Add(players[players.Count - 1]);
            }

            for (int i = 0; i < players.Count - 1; i += 2)
            {
                MatchResult matchResult = matchRuleStrategy.PlayMatch(players[i], players[i + 1], tournament.SurfaceType);
                DistributeExperiencePoints(matchResult);
                nextRoundPlayers.Add(matchResult.Winner);
            }

            return nextRoundPlayers;
        }

        private void DistributeExperiencePoints(MatchResult matchResult)
        {
            experiencePointsService.DistributeExperiencePoints(matchResult, MATCH_WINNER_EXPERIENCE, MATCH_LOSER_EXPERIENCE);
        }

        private void LogTournamentInfo(Tournament tournament, Player winner)
        {
            Console.WriteLine($"The winner of the elimination tournament id:{tournament.Id} is Player {winner.Id}.");
        }
    }
}