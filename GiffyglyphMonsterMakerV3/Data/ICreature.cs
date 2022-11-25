using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GiffyglyphMonsterMakerV3.Pages;
using GiffyglyphMonsterMakerV3.Utility;
using Microsoft.EntityFrameworkCore;

namespace GiffyglyphMonsterMakerV3.Data
{
    public interface ICreature : INotifyPropertyChanged
    {
        [Required]
        public Guid Id { get; init; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int CombatLevel { get; set; }
        [Required]
        public Rank MonsterRank { get; set; }
        public Role MonsterRole { get; set; }
        public string MonsterRoleDetail { get; set; }
        [Required]
        public AttributeArray Attributes { get; set; }
        [Required]
        public DefenseArray Defenses { get; set; }
        public int Proficiency { get; }
        public List<IFeature> Features { get; set; }
        public int WalkSpeed { get; set; }
        public Dictionary<MovementType, int> OtherSpeeds { get; set; }
        public int SpeedMod { get; }
        [Required]
        public OffenseArray Offense { get; set; }
        public int InitiativeModifier { get; }
        [Required]
        public SizeType Size { get; set; }
        [Required]
        public CreatureType Type { get; set; }
        public string TypeDetail { get; set; }
        public Dictionary<SenseType, int> Senses { get; set; }
        public List<string> Languages { get; set; }
        public List<string> Items { get; set; }
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
        Undercommon
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
        public bool Strength { get; set; }
        public bool Dexterity { get; set; }
        public bool Constitution { get; set; }
        public bool Intelligence { get; set; }
        public bool Wisdom { get; set; }
        public bool Charisma { get; set; }
    }
    public class DefenseArray
    {
        public int ArmorClass { get; set; }
        public int HitPoints { get; set; }
        public int SaveBonus { get; set; }
        public ProficientSaves ProficientSavingThrows { get; set; } = new();
    }
    public class OffenseArray : INotifyPropertyChanged
    {
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
