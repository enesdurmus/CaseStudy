using Case.PlayersModule.Models;
using Case.TournamentsModule.Models;
using System.Text.Json.Serialization;

namespace Case.Models
{
    public class TournamentInput
    {
        [JsonPropertyName("players")]
        public Player[] Players { get; set; }

        [JsonPropertyName("tournaments")]
        public Tournament[] Tournaments { get; set; }
    }
}
