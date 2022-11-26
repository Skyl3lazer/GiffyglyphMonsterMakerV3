using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GiffyglyphMonsterMakerV3.Data
{
    public class Feature
    {
        [Key]
        public Guid Id { get; init; }
        public string Name { get; set; } = "";
        public virtual string MarkupDescription { get; } = "";
        public string OverrideMarkup { get; set; } = "";
        public virtual FeatureType Type { get; init; }
        public RarityType Rarity { get; set; }
        public AttributeType RelevantAttribute { get; set; }
        public string Icon { get; set; } = "";
        public virtual string RarityStyle { get; } = "";
        public bool HasSave { get; set; }
        public string SaveVs { get; set; } = "";
        public FeatureFrequency Frequency { get; set; } = new();
        private Creature _parent;
        [Required]
        [BackingField(nameof(_parent))]
        public virtual Creature Parent
        {
            get => _parent;
            init => _parent = value;
        }
        public Guid ParentId { get; init; }
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
        [Key]
        public Guid Id { get; set; }
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
