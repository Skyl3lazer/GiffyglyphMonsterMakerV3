﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GiffyglyphMonsterMakerV3.Pages;
using GiffyglyphMonsterMakerV3.Utility;
using Microsoft.EntityFrameworkCore;

namespace GiffyglyphMonsterMakerV3.Data
{
    public class Creature : INotifyPropertyChanged
    {
        public Creature(string createUserId)
        {
            CreateUserId = createUserId;
        }
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();
        [ForeignKey("Id")]
        public string CreateUserId { get; set; }

        [Required] public string Name { get; set; } = "";
        [Required]
        public int CombatLevel { get; set; }
        private Rank _monsterRank;
        [Required]
        public Rank MonsterRank
        {
            get => _monsterRank;
            set
            {
                _monsterRank = value; Attributes.AttributeMod = MonsterRank == Rank.Elite ? 1 : MonsterRank == Rank.Paragon ? 2 : 0;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(MonsterRank)));
            }
        }
        public Role MonsterRole { get; set; }
        public string MonsterRoleDetail { get; set; } = "";
        [Required]
        public AttributeArray Attributes { get; set; } = new();
        [Required]
        public DefenseArray Defenses { get; set; } = new();
        public int Proficiency => (int)Math.Floor(1 + ((double)CombatLevel + 3) / 4.0);
        private ICollection<Feature> _features = new List<Feature>();
        public ICollection<Feature> Features
        {
            get => _features;
            set
            {
                _features = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(Features)));
            }
        }

        public int WalkSpeed { get; set; } = 30;
        [ForeignKey("SpeedsId")]
        public Dictionary<MovementType, int> OtherSpeeds { get; set; } = new();

        public virtual int SpeedMod { get; } = 0;
        [Required]
        public OffenseArray Offense { get; set; } = new();

        public virtual int InitiativeModifier { get; } = 0;
        [Required]
        public SizeType Size { get; set; }
        [Required]
        public CreatureType Type { get; set; }

        public string TypeDetail { get; set; } = "";
        [ForeignKey("SensesId")]
        public Dictionary<SenseType, int> Senses { get; set; } = new();
        public List<string> Languages { get; set; } = new();
        public List<string> Items { get; set; } = new();

        public int ParagonThreat { get; set; } = 3;
        public int ParagonPowers { get; set; } = 0;
        public int ParagonDefenses { get; set; } = 0;

        public LayoutType Layout { get; set; } = LayoutType.Single;

        public int MaxRange
        {
            get
            {
                var rangedFeatures = Features.Where(a =>
                    a.Type == FeatureType.Action &&
                    ((Action)a).Distance == RangeType.Ranged).Select(b => (Action)b);
                rangedFeatures.OrderByDescending(b => b.Distance);
                return rangedFeatures.Count() > 0 ? rangedFeatures.First().Range : 0;
            }
        }

        public int MaxReach
        {
            get
            {
                var meleeFeatures = Features.Where(a =>
                    a.Type == FeatureType.Action &&
                    ((Action)a).Distance == RangeType.Melee).Select(b => (Action)b);
                meleeFeatures.OrderByDescending(b => b.Distance);
                return meleeFeatures.Count() > 0 ? meleeFeatures.First().Range : 0;
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
    }

    public enum LayoutType
    {
        Single = 0,
        TwoColumn
    }
    public enum Rank
    {
        Minion = 0,
        Grunt,
        Elite,
        Paragon
    }
    public enum Role
    {
        Controller = 0,
        Defender,
        Lurker,
        Skirmisher,
        Striker,
        Supporter
    }

    public enum DefaultLanguages
    {
        Common,
        Undercommon,
        Abyssal,
        Celestial,
        Deep,
        Draconic,
        Dwarvish,
        Elvish,
        Giant,
        Gnomish,
        Goblin,
        Halfling,
        Infernal,
        Orc,
        Primordial,
        Sylvan
    }

    public enum AttributeType
    {
        Strength = 0,
        Dexterity,
        Constitution,
        Intelligence,
        Wisdom,
        Charisma
    }
    public class AttributeArray : INotifyPropertyChanged
    {
        [Key]
        public Guid Id { get; set; }
        public Dictionary<AttributeType, int> Dict { get; }

        public AttributeArray()
        {
            Dict = new ();
            Dict[AttributeType.Strength] = 0;
            Dict[AttributeType.Constitution] = 0;
            Dict[AttributeType.Intelligence] = 0;
            Dict[AttributeType.Wisdom] = 0;
            Dict[AttributeType.Charisma] = 0;
            Dict[AttributeType.Dexterity] = 0;
        }
        
        public int AttributeMod { get; set; }
        public int Strength
        {
            get => Dict[AttributeType.Strength];
            set
            {
                Dict[AttributeType.Strength] = value;
                PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(nameof(Strength)));
            }
        }
        public int Dexterity
        {
            get => Dict[AttributeType.Dexterity];
            set {
                Dict[AttributeType.Dexterity] = value; PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(nameof(Dexterity)));
            }
        }
        public int Constitution
        {
            get => Dict[AttributeType.Constitution];
            set
            {
                Dict[AttributeType.Constitution] = value; PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(nameof(Constitution)));
            }
        }
        public int Intelligence
        {
            get => Dict[AttributeType.Intelligence];
            set {
                Dict[AttributeType.Intelligence] = value; PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(nameof(Intelligence)));
            }
        }
        public int Wisdom
        {
            get => Dict[AttributeType.Wisdom];
            set {
                Dict[AttributeType.Wisdom] = value; PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(nameof(Wisdom)));
            }
        }
        public int Charisma
        {
            get => Dict[AttributeType.Charisma];
            set {
                Dict[AttributeType.Charisma] = value; PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(nameof(Charisma)));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
    public class ProficientSaves
    {
        [Key]
        public Guid Id { get; set; }
        public bool Strength { get; set; }
        public bool Dexterity { get; set; }
        public bool Constitution { get; set; }
        public bool Intelligence { get; set; }
        public bool Wisdom { get; set; }
        public bool Charisma { get; set; }
    }
    public class DefenseArray
    {
        [Key]
        public Guid Id { get; set; }
        public int ArmorClass { get; set; }
        public int HitPoints { get; set; }
        public int SaveBonus { get; set; }
        public ProficientSaves ProficientSavingThrows { get; set; } = new();
    }
    public class OffenseArray : INotifyPropertyChanged
    {
        [Key]
        public Guid Id { get; set; }
        private int _attack;
        private int _difficultyCheck;
        private int _damage;
        private bool _randomizeDamage;
        private DamageRange _randomDamageRange;
        public int Attack
        {
            get => _attack;
            set { _attack = value; PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(nameof(Attack)));
            }
        }

        public int DifficultyCheck
        {
            get => _difficultyCheck;
            set { _difficultyCheck = value; PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(nameof(DifficultyCheck)));
            }
        }

        public int Damage
        {
            get => _damage;
            set { _damage = value; PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(nameof(Damage)));
            }
        }

        public bool RandomizeDamage
        {
            get => _randomizeDamage;
            set { _randomizeDamage = value; PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(nameof(RandomizeDamage)));
            }
        }

        public DamageRange RandomDamageRange
        {
            get => _randomDamageRange;
            set { _randomDamageRange = value; PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(nameof(RandomDamageRange)));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }

    public enum MovementType
    {
        Swim = 0,
        Fly,
        Climb
    }

    public enum SenseType
    {
        blindsight = 0,
        darkvision,
        tremorsense,
        truesight
    }
    public enum SizeType
    {
        Tiny = 0,
        Small,
        Medium,
        Large,
        Huge,
        Gargantuan
    }
    public enum CreatureType
    {
        Aberration = 0,
        Beast,
        Celestial,
        Construct,
        Dragon,
        Elemental,
        Fey,
        Fiend,
        Giant,
        Humanoid,
        Monstrosity,
        Ooze,
        Plant,
        Undead
    }
}
