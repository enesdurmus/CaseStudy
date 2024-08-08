using Case.PlayersModule.Enums;
using Case.PlayersModule.Models;
using Case.Systems.RandomGenerator.Interfaces;
using Case.TournamentsModule.Enums;
using Case.TournamentsModule.Interfaces;
using Case.TournamentsModule.Models;

namespace Case.TournamentsModule.Strategies.MatchStrategies
{
    public class DefaultMatchRuleStrategy : IMatchRuleStrategy
    {
        private const int MATCH_SCORE = 1;
        private const int LEFT_HAND_SCORE = 2;
        private const int EXPERIENCE_SCORE = 3;
        private const int ABILITY_SCORE = 4;

        private readonly IRandomGenerator randomGenerator;

        public DefaultMatchRuleStrategy(IRandomGenerator randomGenerator)
        {
            this.randomGenerator = randomGenerator;
        }

        public MatchResult PlayMatch(Player player1, Player player2, SurfaceType surfaceType)
        {
            int player1Score = CalculatePlayerScore(player1, player2, surfaceType);
            int player2Score = CalculatePlayerScore(player2, player1, surfaceType);

            if (DetermineWinner(player1Score, player2Score))
            {
                //Console.WriteLine($"Winner of the match is Player {player1.Id} against {player2.Id}.");
                return new MatchResult(player1, player2);
            }

            //Console.WriteLine($"Winner of the match is Player {player2.Id} against {player1.Id}.");
            return new MatchResult(player2, player1);
        }

        private static int CalculatePlayerScore(Player player, Player opponent, SurfaceType surfaceType)
        {
            int score = MATCH_SCORE;

            score += CalculateHandednessScore(player);
            score += CalculateExperienceScore(player, opponent);
            score += CalculateAbilityScore(player, opponent, surfaceType);

            return score;
        }

        private static int CalculateHandednessScore(Player player)
        {
            return player.Hand == Handedness.LEFT ? LEFT_HAND_SCORE : 0;
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

        private bool DetermineWinner(int value1, int value2)
        {
            int sum = value1 + value2;
            double ratio = (double) value1 / sum;

            return randomGenerator.NextDouble() <= ratio;
        }
    }
}
