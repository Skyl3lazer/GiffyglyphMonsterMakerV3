using System;
using System.ComponentModel;
using GiffyglyphMonsterMakerV3.Pages;
using GiffyglyphMonsterMakerV3.Utility;

namespace GiffyglyphMonsterMakerV3.Data
{
    public interface ICreature : INotifyPropertyChanged
    {
        public Guid ID { get; init; }
        public string Name { get; set; }
        public int CombatLevel { get; set; }
        public Rank MonsterRank { get; set; }
        public Role MonsterRole { get; set; }
        public string MonsterRoleDetail { get; set; }
        public AttributeArray Attributes { get; set; }
        public DefenseArray Defenses { get; set; }
        public int Proficiency { get; }
        public List<IFeature> Features { get; set; }
        public int WalkSpeed { get; set; }
        public Dictionary<MovementType, int> OtherSpeeds { get; set; }
        public int SpeedMod { get; }
        public OffenseArray Offense { get; set; }
        public int InitiativeModifier { get; }
        public SizeType Size { get; set; }
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
    public class AttributeArray : INotifyPropertyChanged
    {
        private int _strength;
        private int _dexterity;
        private int _constitution;
        private int _intelligence;
        private int _wisdom;
        private int _charisma;
        
        public int AttributeMod { get; set; }

        public int HighStat
        {
            get
            {
                return (int)Math.Max(Dexterity, Math.Max(Strength, Math.Max(Charisma, Math.Max(Constitution, Math.Max(Intelligence,Wisdom)))));
            }
        }
        public int Strength
        {
            get => _strength;
            set { _strength = value;
                PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(nameof(Strength)));
            }
        }
        public int Dexterity
        {
            get => _dexterity;
            set { _dexterity = value; PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(nameof(Dexterity)));
            }
        }
        public int Constitution
        {
            get => _constitution;
            set { _constitution = value; PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(nameof(Constitution)));
            }
        }
        public int Intelligence
        {
            get => _intelligence;
            set { _intelligence = value; PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(nameof(Intelligence)));
            }
        }
        public int Wisdom
        {
            get => _wisdom;
            set { _wisdom = value; PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(nameof(Wisdom)));
            }
        }
        public int Charisma
        {
            get => _charisma;
            set { _charisma = value; PropertyChanged?.Invoke(this,
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
