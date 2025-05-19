using Newtonsoft.Json;
using System;
using System.ComponentModel;
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
        /// <summary>
        /// Returns a descriptive string when printed, or the name of the enum
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToDescriptionString(this Enum val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
               .GetType()
               .GetField(val.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : val.ToString();
        }
        /// <summary>
        /// Gets all items for an enum value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static IEnumerable<T> GetAllItems<T>(this Enum value)
        {
            foreach (object item in Enum.GetValues(typeof(T)))
            {
                yield return (T)item;
            }
        }

    }
    public class Enum<T> where T : struct, IConvertible
    {
        /// <summary>
        /// Gets all items for an enum value in pretty form
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static IEnumerable<string> GetAllItemsDescriptiveNames()
        {
            foreach (object item in Enum.GetValues(typeof(T)))
            {
                yield return ((Enum)item).ToDescriptionString();
            }
        }

    }
}
