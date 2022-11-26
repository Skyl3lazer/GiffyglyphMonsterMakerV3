using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace GiffyglyphMonsterMakerV3.Data;

public class Monster : Creature
{
    public Monster(string name)
    {
        Name = name;
        Id = Guid.NewGuid();
        Offense = new OffenseArray();
        Defenses = new DefenseArray();
        OtherSpeeds = new Dictionary<MovementType, int>();
        Senses = new Dictionary<SenseType, int>();
        Languages = new List<string>();
        Items = new List<string>();
        Features.Add(new Action()
        {
            Name = "Hit Them",
            Rarity = RarityType.Common,
            Range = 5,
            Distance = RangeType.Melee,
            Icon = "fa-sword",
            Targets = 1,
            ActionDamageType = DamageType.bludgeoning,
            DealsDamage = true,
            Shape = TargetShape.target,
            RelevantAttribute = AttributeType.Strength,
            Parent = this
        });
        Features.Add(new Action()
        {
            Name = "Hit Them Twice",
            Rarity = RarityType.Uncommon,
            Range = 5,
            MultiAttack = 2,
            Distance = RangeType.Melee,
            Icon = "fa-sword",
            Targets = 1,
            ActionDamageType = DamageType.bludgeoning,
            DealsDamage = true,
            Shape = TargetShape.target,
            RelevantAttribute = AttributeType.Dexterity,
            Frequency = new FeatureFrequency()
            {
                Type = FrequencyType.shortrest,
                Value = 2
            },
            Parent = this
        });
        Features.Add(new Action()
        {
            Name = "Deadly Spell",
            Rarity = RarityType.Rare,
            Range = 30,
            Distance = RangeType.Ranged,
            HasSave = true,
            SaveVs = "DEX",
            Icon = "fa-bow-arrow",
            Targets = 0,
            ActionDamageType = DamageType.psychic,
            RelevantAttribute = AttributeType.Intelligence,
            DealsDamage = true,
            Frequency = new FeatureFrequency()
            {
                Type = FrequencyType.cooldown,
                Value = 3
            },
            Parent = this
        });

        Features.Add(new BonusAction()
        {
            Name = "Bonus Smack",
            Rarity = RarityType.Common,
            Range = 5,
            Distance = RangeType.Melee,
            Icon = "fa-sword",
            Targets = 1,
            ActionDamageType = DamageType.bludgeoning,
            RelevantAttribute = AttributeType.Strength,
            DealsDamage = true,
            Shape = TargetShape.target,
            DamageMultiplier = 0.5,
            Parent = this
        });
    }

    public Monster()
    {
        Name = "My New Monster";
        Id = Guid.NewGuid();
        Offense = new OffenseArray();
        Defenses = new DefenseArray();
        OtherSpeeds = new Dictionary<MovementType, int>();
        Senses = new Dictionary<SenseType, int>();
        Languages = new List<string>();
        Items = new List<string>();
        Features.Add(new Action()
        {
            Name = "Hit Them",
            Rarity = RarityType.Common,
            Range = 5,
            Distance = RangeType.Melee,
            Icon = "fa-sword",
            Targets = 1,
            ActionDamageType = DamageType.bludgeoning,
            DealsDamage = true,
            Shape = TargetShape.target,
            RelevantAttribute = AttributeType.Strength,
            Parent = this
        });
        
        Senses.Add(SenseType.darkvision, 30);
    }
    
    
    public override int SpeedMod
    {
        get
        {
            if (MonsterRole == Role.Defender)
                return -5;
            if (MonsterRole == Role.Skirmisher)
                return 5;
            return 0;
        }
    }
    
    public override int InitiativeModifier
    {
        get
        {
            var mod = 0;

            if (MonsterRole == Role.Controller || MonsterRole == Role.Supporter)
                mod += Proficiency;

            if (MonsterRank == Rank.Elite)
                mod += (int)Math.Floor((double)Proficiency / 2);
            else if (MonsterRank == Rank.Paragon)
                mod += Proficiency;

            return mod;
        }
    }
}