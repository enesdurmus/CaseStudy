using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Case.PlayersModule.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Handedness
    {
        [EnumMember(Value = "left")]
        LEFT = 0,

        [EnumMember(Value = "right")]
        RIGHT = 1
    }
}
