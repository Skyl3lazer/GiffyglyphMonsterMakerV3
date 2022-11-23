﻿namespace GiffyglyphMonsterMakerV3.Data
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
    }

    public enum RangeType
    {
        Melee = 0,
        Ranged
    }
}
