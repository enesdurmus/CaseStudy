using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Case.TournamentsModule.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SurfaceType
    {
        [EnumMember(Value = "clay")]
        CLAY = 0,

        [EnumMember(Value = "grass")]
        GRASS = 1,

        [EnumMember(Value = "hard")]
        HARD = 2
    }
}
