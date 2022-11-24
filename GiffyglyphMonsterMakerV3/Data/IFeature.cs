﻿namespace GiffyglyphMonsterMakerV3.Data
{
    public interface IFeature
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public FeatureType Type { get; set; }
        public RarityType Rarity { get; set; }
        public string Icon { get; set; }
        public string RarityStyle { get; }
    }
    public enum FeatureType
    {
        Trait = 0,
        Free,
        Bonus,
        Action,
        Reaction,
        Countermeasure
    }
    public enum RarityType
    {
        Common = 0,
        Uncommon,
        Rare
    }
}
