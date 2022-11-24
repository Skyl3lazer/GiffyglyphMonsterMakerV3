using Microsoft.AspNetCore.Components;

namespace GiffyglyphMonsterMakerV3.Data
{
    public class Action : IFeature
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public FeatureType Type { get; set; } = FeatureType.Action;
        public RangeType Distance { get; set; }
        public int Range { get; set; }
        public RarityType Rarity { get; set; }
        public string Icon { get; set; }
        public bool IsSpell { get; set; }
        public string RarityStyle{
            get
            {
                string style = "";
                switch (Rarity)
                {
                    case RarityType.Common: style += "ability-common rounded-circle";
                        break;
                    case RarityType.Uncommon: style += "ability-uncommon rounded-end rounded-start-bottom";
                        break;
                    case RarityType.Rare: style += "ability-rare rounded-end-top rounded-start-bottom";
                        break;
                    default:
                        style += "";
                        break;
                }
                return style;
            }
        }
    }

    public enum RangeType
    {
        Melee = 0,
        Ranged
    }
}
