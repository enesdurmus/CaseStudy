using Case.Systems.SimpleIoCContainer.Core;
using Case.TournamentsModule.Enums;
using Case.TournamentsModule.Interfaces;
using Case.TournamentsModule.Strategies.TournamentStrategies;
using IServiceProvider = Case.Systems.SimpleIoCContainer.Interfaces.IServiceProvider;

namespace Case.TournamentsModule.Factory
{
    public class TournamentStrategyFactory
    {
        private readonly IServiceProvider serviceProvider;

        public TournamentStrategyFactory()
        {
            this.serviceProvider = IoCContainer.Instance;
        }

        public ITournamentStrategy CreateTournamentStrategy(TournamentStrategy tournamentStrategy)
        {
            return tournamentStrategy switch
            {
                TournamentStrategy.ELIMINATION => serviceProvider.GetService<EliminationTournamentStrategy>(),
                TournamentStrategy.LEAGUE => serviceProvider.GetService<LeagueTournamentStrategy>(),
                _ => throw new ArgumentException("Invalid tournament strategy"),
            };
        }
    }
}