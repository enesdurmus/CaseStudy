using Case.Models;
using Case.PlayersModule.Models;

namespace Case.TournamentsModule.Services
{
    public class SortingRuleService
    {
        public ResultsData SortPlayers(Player[] players)
        {
            List<Player> sortedPlayers = SortPlayersList(players);

            ResultsData resultsData = GenerateResultsData(sortedPlayers);

            PrintFinalStandings(sortedPlayers);

            return resultsData;
        }

        private List<Player> SortPlayersList(Player[] players)
        {
            return players
                .OrderByDescending(p => p.Experience - p.InitialExperience)
                .ThenByDescending(p => p.InitialExperience)
                .ToList();
        }

        private ResultsData GenerateResultsData(List<Player> sortedPlayers)
        {
            var results = sortedPlayers.Select((player, index) => new PlayerResult
            {
                Order = index + 1,
                PlayerId = player.Id,
                GainedExperience = player.Experience - player.InitialExperience,
                TotalExperience = player.Experience
            }).ToArray();

            return new ResultsData { Results = results };
        }

        private void PrintFinalStandings(List<Player> sortedPlayers)
        {
            Console.WriteLine("Final Standings:");

            foreach (Player player in sortedPlayers)
            {
                Console.WriteLine($"Player {player.Id} - Initial Experience: {player.InitialExperience}, Gained Experience: {player.Experience - player.InitialExperience}, Total Experience: {player.Experience}");
            }
        }
    }
}
