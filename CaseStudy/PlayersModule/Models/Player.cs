using Newtonsoft.Json;
using Case.PlayersModule.Enums;
using Case.PlayersModule.Utilities;
using Case.TournamentsModule.Enums;
using System.Runtime.Serialization;

namespace Case.PlayersModule.Models
{
    public class Player
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("hand")]
        public Handedness Hand { get; set; }

        [JsonProperty("experience")]
        public int Experience { get; set; }

        public int InitialExperience { get; private set; }


        [JsonConverter(typeof(SkillsConverter))]
        [JsonProperty("skills")]
        public Dictionary<SurfaceType, int> Skills { get; set; }

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            InitialExperience = Experience;
        }
    }
}
