using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GiffyglyphMonsterMakerV3.Utility;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace GiffyglyphMonsterMakerV3.Data
{
    public class Feature
    {
        [Key]
        public Guid Id { get; set; }
        public Guid? TemplateId { get; set; }
        public string Name { get; set; } = "";

        public virtual string MarkupDescription(Creature parentCreature)
        {
            return "";
        }
        public string OverrideMarkup { get; set; } = "";
        public virtual FeatureType Type { get; init; }
        public RarityType Rarity { get; set; }
        public AttributeType RelevantAttribute { get; set; }
        public virtual string Icon { get; } = "";
        public string CustomIcon { get; set; } = "";
        public virtual string RarityStyle
        {
            get
            {
                string style = "";
                switch (Rarity)
                {
                    case RarityType.Common:
                        style += "ability-common rounded-circle";
                        break;
                    case RarityType.Uncommon:
                        style += "ability-uncommon rounded-end rounded-start-bottom";
                        break;
                    case RarityType.Rare:
                        style += "ability-rare rounded-end-top rounded-start-bottom";
                        break;
                    default:
                        style += "";
                        break;
                }
                return style;
            }
        }
        public bool HasSave { get; set; }
        public string SaveVs { get; set; } = "";
        public FeatureFrequency Frequency { get; set; } = new();
        [Required]
        [ForeignKey("Id")]
        public Guid ParentId { get; set; }

        public virtual void UpdateThisToMatch(object o)
        {
            if (o is not Feature f)
                throw new InvalidDataException("Target is not a feature");

            Name = f.Name;
            OverrideMarkup = f.OverrideMarkup;
            Rarity = f.Rarity;
            RelevantAttribute = f.RelevantAttribute;
            CustomIcon = f.CustomIcon;
            HasSave = f.HasSave;
            SaveVs = f.SaveVs;
            Frequency = f.Frequency.Clone();
            Frequency.Id = Guid.NewGuid();
        }
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
