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
        public static T To<T>(this object obj)
        {
            Type t = typeof(T);
            Type u = Nullable.GetUnderlyingType(t);

            if (u != null)
            {
                return (obj == null) ? default(T) : (T)Convert.ChangeType(obj, u);
            }
            else
            {
                return (T)Convert.ChangeType(obj, t);
            }
        }
    }
}
