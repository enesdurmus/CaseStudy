namespace Case.Systems.RandomGenerator.Interfaces
{
    public interface IRandomGenerator
    {
        public int Next(int count);

        public int NextInt(int minValue, int maxValue);

        public double NextDouble();
    }
}
