using Case.TournamentsModule.Models;

namespace Case.TournamentsModule.Interfaces
{
    public interface IExperiencePointsService
    {
        public void DistributeExperiencePoints(MatchResult matchResult, int winnerExperience, int loserExperience);
    }
}
