using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace GiffyglyphMonsterMakerV3.Utility
{
    public class Extensions
    {
    }
    public static class SystemExtension
    {
        public static T Clone<T>(this T source)
        {
            var serialized = JsonSerializer.Serialize(source);
            return JsonSerializer.Deserialize<T>(serialized);
        }
        public static T To<T>(this object obj)
        {
            var item = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj));
            return item;
        }
    }
}
