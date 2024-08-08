using Case.PlayersModule.Models;
using Case.Systems.RandomGenerator.Interfaces;
using Case.TournamentsModule.Interfaces;
using Case.TournamentsModule.Models;

namespace Case.TournamentsModule.Strategies.TournamentStrategies
{
    public class LeagueTournamentStrategy : ITournamentStrategy
    {
        private const int MATCH_WINNER_EXPERIENCE = 10;
        private const int MATCH_LOSER_EXPERIENCE = 1;

        private readonly IRandomGenerator randomGenerator;
        private readonly IExperiencePointsService experiencePointsService;

        private readonly List<MatchResult> matchResults;

        public LeagueTournamentStrategy(IRandomGenerator randomGenerator, IExperiencePointsService experiencePointsService)
        {
            this.randomGenerator = randomGenerator;
            this.experiencePointsService = experiencePointsService;

            matchResults = new List<MatchResult>();
        }

        public void MatchPlayers(Tournament tournament, Player[] contenders, IMatchRuleStrategy matchRuleStrategy)
        {
            List<Match> matches = ScheduleMatches(contenders);

            while (matches.Count > 0)
            {
                int index = randomGenerator.Next(matches.Count);
                MatchResult matchResult = matchRuleStrategy.PlayMatch(matches[index].Player1, matches[index].Player2, tournament.SurfaceType);
                matchResults.Add(matchResult);
                DistributeExperiencePoints(matchResult);
                matches.RemoveAt(index);
            }

            LogTournamentInfo(tournament);
        }

        private void DistributeExperiencePoints(MatchResult matchResult)
        {
            experiencePointsService.DistributeExperiencePoints(matchResult, MATCH_WINNER_EXPERIENCE, MATCH_LOSER_EXPERIENCE);
        }

        private List<Match> ScheduleMatches(Player[] contenders)
        {
            if (contenders.Length % 2 != 0)
            {
                //Bye geçme logic'i eklenebilir.
            }

            int numRounds = contenders.Length - 1;

            List<Player> contendersList = new List<Player>(contenders);
            List<Match> matches = new List<Match>();

            for (int round = 0; round < numRounds; round++)
            {
                for (int i = 0; i < contendersList.Count / 2; i++)
                {
                    Match match = new Match(contendersList[i], contendersList[contendersList.Count - 1 - i]);
                    matches.Add(match);
                }

                RotatePlayers(contendersList);
            }

            return matches;
        }

        private static void RotatePlayers(List<Player> players)
        {
            Player temp = players[players.Count - 1];
            players.RemoveAt(players.Count - 1);
            players.Insert(1, temp);
        }

        private Player DetermineWinner()
        {
            Dictionary<Player, int> wins = new Dictionary<Player, int>();

            foreach (var matchResult in matchResults)
            {
                wins[matchResult.Winner] = wins.GetValueOrDefault(matchResult.Winner) + 1;
            }

            Player winner = wins.FirstOrDefault(x => x.Value == wins.Values.Max()).Key;
            return winner;
        }

        private void LogTournamentInfo(Tournament tournament)
        {
            Player winner = DetermineWinner();
            Console.WriteLine($"The winner of the league tournament id:{tournament.Id} is Player {winner.Id}.");
        }
    }
}
