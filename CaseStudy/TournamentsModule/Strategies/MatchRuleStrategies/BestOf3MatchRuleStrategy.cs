using Case.PlayersModule.Models;
using Case.Systems.RandomGenerator.Interfaces;
using Case.TournamentsModule.Enums;
using Case.TournamentsModule.Interfaces;
using Case.TournamentsModule.Models;

namespace Case.TournamentsModule.Strategies.MatchStrategies
{
    public class BestOf3MatchRuleStrategy : IMatchRuleStrategy
    {
        private const int EXPERIENCE_SCORE = 5;
        private const int ABILITY_SCORE = 4;

        private readonly IRandomGenerator randomGenerator;

        public BestOf3MatchRuleStrategy(IRandomGenerator randomGenerator)
        {
            this.randomGenerator = randomGenerator;
        }

        public MatchResult PlayMatch(Player player1, Player player2, SurfaceType surfaceType)
        {
            int player1WinCount = 0;

            for (int i = 0; i < 3; i++)
            {
                int player1Score = CalculatePlayerScore(player1, player2, surfaceType);
                int player2Score = CalculatePlayerScore(player2, player1, surfaceType);
                if (DetermineWinner(player1Score, player2Score))
                {
                    player1WinCount++;
                }

                if (player1WinCount >= 2)
                {
                    return new MatchResult(player1, player2);
                }
            }

            return new MatchResult(player2, player1);
        }

        private int CalculatePlayerScore(Player player, Player opponent, SurfaceType surfaceType)
        {
            int score = 0;
            score += CalculateExperienceScore(player, opponent);
            score += CalculateAbilityScore(player, opponent, surfaceType);
            return score;
        }

        private static int CalculateExperienceScore(Player player, Player opponent)
        {
            return player.Experience > opponent.Experience ? EXPERIENCE_SCORE : 0;
        }

        private static int CalculateAbilityScore(Player player, Player opponent, SurfaceType surfaceType)
        {
            if (!player.Skills.ContainsKey(surfaceType) || !opponent.Skills.ContainsKey(surfaceType))
            {
                throw new ArgumentException("Players must have skills for the given surface type.");
            }

            return player.Skills[surfaceType] > opponent.Skills[surfaceType] ? ABILITY_SCORE : 0;
        }

        public bool DetermineWinner(int value1, int value2)
        {
            int sum = value1 + value2;
            double ratio = (double) value1 / sum;

            return randomGenerator.NextDouble() <= ratio;
        }
    }
}
