namespace GiffyglyphMonsterMakerV3.Data
{
    public interface ICreature
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
    public class AttributeArray
    {
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }
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
    public class OffenseArray
    {
        public int Attack { get; set; }
        public int DifficultyCheck { get; set; }
        public int Damage { get; set; }
        public int MaxRange { get;  }
        public int MaxReach { get; }
    }

    public enum MovementType
    {
        Swim = 0,
        Fly,
        Climb
    }

    public enum SenseType
    {
        darkvision = 0,
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
