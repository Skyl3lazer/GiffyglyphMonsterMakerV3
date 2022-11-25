namespace GiffyglyphMonsterMakerV3.Data
{
    public interface IFeature
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public string MarkupDescription { get; }
        public string OverrideMarkup { get; set; }
        public FeatureType Type { get; init; }
        public RarityType Rarity { get; set; }
        public string Icon { get; set; }
        public string RarityStyle { get; }
        public bool HasSave { get; set; }
        public string SaveVs { get; set; }
        public FeatureFrequency Frequency { get; set; }
        public ICreature Parent { get; init; }
    }
    public enum FeatureType
    {
        Trait = 0,
        Free,
        Action,
        Bonus,
        Reaction,
        Countermeasure
    }
    public enum RarityType
    {
        Common = 0,
        Uncommon,
        Rare
    }

    public enum FrequencyType
    {
        passive = 0,
        cooldown,
        shortrest,
        longrest
    }
    public class FeatureFrequency
    {
        public FrequencyType Type { get; set; }
        public int Value { get; set; }

        public string StringValue
        {
            get
            {
                string desc = "";
                switch (Type)
                {
                    case FrequencyType.passive:
                        break;
                    case FrequencyType.cooldown:
                        desc += "Cooldown "+Value;
                        break;
                    case FrequencyType.shortrest:
                        desc += Value + "/sr";
                        break;
                    case FrequencyType.longrest:
                        desc += Value + "/lr";
                        break;
                }

                return desc;
            }
        }
    }
}
