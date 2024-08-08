using Case.Models;
using Case.Systems.RandomGenerator.Interfaces;
using Case.Systems.RandomGenerator.Services;
using Case.Systems.SimpleIoCContainer.Core;
using Case.Systems.SimpleIoCContainer.Enums;
using Case.Systems.SimpleIoCContainer.Interfaces;
using Case.TournamentsModule.Factory;
using Case.TournamentsModule.Interfaces;
using Case.TournamentsModule.Models;
using Case.TournamentsModule.Services;
using Case.TournamentsModule.Strategies.MatchStrategies;
using Case.TournamentsModule.Strategies.TournamentStrategies;
using Case.Utilities;

namespace Case
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            ConfigureServices();
            await RunTournaments();

            Console.WriteLine("\nPress any key to continue... ");
            Console.ReadKey();
        }

        private static void ConfigureServices()
        {
            IServiceCollection serviceCollection = IoCContainer.Instance;
            serviceCollection.Register<IRandomGenerator, RandomGenerator>(ServiceLifetime.Singleton);
            serviceCollection.Register<IExperiencePointsService, ExperiencePointsService>(ServiceLifetime.Singleton);

            serviceCollection.Register<DefaultMatchRuleStrategy, DefaultMatchRuleStrategy>(ServiceLifetime.Transient);
            serviceCollection.Register<BestOf3MatchRuleStrategy, BestOf3MatchRuleStrategy>(ServiceLifetime.Transient);

            serviceCollection.Register<EliminationTournamentStrategy, EliminationTournamentStrategy>(ServiceLifetime.Transient);
            serviceCollection.Register<LeagueTournamentStrategy, LeagueTournamentStrategy>(ServiceLifetime.Transient);

            serviceCollection.Register<MatchRulesStrategyFactory, MatchRulesStrategyFactory>(ServiceLifetime.Singleton);
            serviceCollection.Register<TournamentStrategyFactory, TournamentStrategyFactory>(ServiceLifetime.Singleton);

            serviceCollection.Register<TournamentService, TournamentService>(ServiceLifetime.Singleton);
            serviceCollection.Register<SortingRuleService, SortingRuleService>(ServiceLifetime.Singleton);
        }

        public static async Task RunTournaments()
        {
            TournamentInput tournamentInput = await ReadInput();
            TournamentService tournamentService = IoCContainer.Instance.GetService<TournamentService>();

            foreach (Tournament tournament in tournamentInput.Tournaments)
            {
                tournamentService.StartTournament(tournament, tournamentInput.Players);
            }

            SortingRuleService sortingRuleService = IoCContainer.Instance.GetService<SortingRuleService>();
            ResultsData resultsData = sortingRuleService.SortPlayers(tournamentInput.Players);
            await WriteResults(resultsData);
        }

        public static async Task<TournamentInput> ReadInput()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\input.json");
            return await JsonFileUtils.ReadAsync<TournamentInput>(path);
        }

        public static async Task WriteResults(ResultsData resultsData)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\output.json");
            await JsonFileUtils.WriteAsync<ResultsData>(path, resultsData);
        }
    }
}
