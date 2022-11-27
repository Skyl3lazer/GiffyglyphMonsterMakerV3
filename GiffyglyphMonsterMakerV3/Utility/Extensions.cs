using System.Text.Json;
using System.Text.Json.Serialization;

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
    }
}
