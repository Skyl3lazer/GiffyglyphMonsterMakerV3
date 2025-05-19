using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace GiffyglyphMonsterMakerV3.Data
{
    public class LocalStorage
    {
        [JsonPropertyName("isDark")]
        public bool isDark { get;set; }
        [JsonPropertyName("lastOpenedMonster")]
        public Guid? lastOpenedMonster { get; set; }
        [JsonPropertyName("lastOpenedFolder")]
        public Guid? lastOpenedFolder { get; set; }
    }
}
