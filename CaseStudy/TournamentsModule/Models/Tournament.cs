using Newtonsoft.Json;
using Case.TournamentsModule.Enums;

namespace Case.TournamentsModule.Models
{
    public class Tournament
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("surface")]
        public SurfaceType SurfaceType { get; set; }

        [JsonProperty("type")]
        public TournamentStrategy TournamentStrategy { get; set; }

        [JsonProperty("match_rule")]
        public MatchRule MatchRule { get; set; } = MatchRule.DEFAULT;
    }
}
