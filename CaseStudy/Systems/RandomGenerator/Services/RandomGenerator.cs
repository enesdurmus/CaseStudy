using Case.Systems.RandomGenerator.Interfaces;

namespace Case.Systems.RandomGenerator.Services
{
    public class RandomGenerator : IRandomGenerator
    {
        private readonly Random random;

        public RandomGenerator()
        {
            random = new Random();
        }

        public int NextInt(int minValue, int maxValue)
        {
            return random.Next(minValue, maxValue);
        }

        public double NextDouble()
        {
            return random.NextDouble();
        }

        public int Next(int count)
        {
            return random.Next(count);
        }
    }
}
