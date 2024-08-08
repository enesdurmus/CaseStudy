using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Case.TournamentsModule.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MatchRule
    {
        [EnumMember(Value = "default")]
        DEFAULT = 0,

        [EnumMember(Value = "best_of_3")]
        BEST_OF_3 = 1
    }
}
