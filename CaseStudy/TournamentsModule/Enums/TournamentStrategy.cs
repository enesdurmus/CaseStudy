using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Case.TournamentsModule.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TournamentStrategy
    {
        [EnumMember(Value = "elimination")]
        ELIMINATION = 0,

        [EnumMember(Value = "league")]
        LEAGUE = 1
    }
}
