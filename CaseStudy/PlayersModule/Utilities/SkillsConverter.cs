using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Case.TournamentsModule.Enums;

namespace Case.PlayersModule.Utilities
{
    public class SkillsConverter : JsonConverter<Dictionary<SurfaceType, int>>
    {
        public override void WriteJson(JsonWriter writer, Dictionary<SurfaceType, int> value, JsonSerializer serializer)
        {
            JObject obj = new JObject();
            foreach (var kvp in value)
            {
                obj.Add(kvp.Key.ToString().ToLower(), kvp.Value);
            }
            obj.WriteTo(writer);
        }

        public override Dictionary<SurfaceType, int> ReadJson(JsonReader reader, Type objectType, Dictionary<SurfaceType, int> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            Dictionary<SurfaceType, int> skills = new Dictionary<SurfaceType, int>();

            foreach (var property in obj.Properties())
            {
                if (Enum.TryParse<SurfaceType>(property.Name, true, out SurfaceType surfaceType))
                {
                    skills[surfaceType] = property.Value.ToObject<int>();
                }
            }

            return skills;
        }
    }
}