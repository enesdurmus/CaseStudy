namespace Case.Models
{
    public class ResultsData
    {
        public ResultsData() { }
        public PlayerResult[] Results { get; set; }
    }

    public class PlayerResult
    {
        public int Order { get; set; }
        public int PlayerId { get; set; }
        public int GainedExperience { get; set; }
        public int TotalExperience { get; set; }
    }
}
