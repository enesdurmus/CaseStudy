using Case.TournamentsModule.Interfaces;
using Case.TournamentsModule.Models;

namespace Case.TournamentsModule.Services
{
    public class ExperiencePointsService : IExperiencePointsService
    {
        public ExperiencePointsService() { }

        public void DistributeExperiencePoints(MatchResult matchResult, int winnerExperience, int loserExperience)
        {
            matchResult.Winner.Experience += winnerExperience;
            matchResult.Loser.Experience += loserExperience;
        }
    }
}
