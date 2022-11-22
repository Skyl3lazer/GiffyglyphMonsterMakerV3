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
        public int Proficiency { get; set; }
        public IEnumerable<IFeature> Features { get; set; }
        public SpeedArray Speed { get; set; }
        public CombatArray Combat { get; set; }
        public int InitiativeModifier { get; set; }
        public SizeType Size { get; set; }
        public CreatureType Type { get; set; }
        public string TypeDetail { get; set; }
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
        public ProficientSaves SavingThrows { get; set; } = new();
    }
    public class SpeedArray
    {
        public int Walk { get; set; }
        public int Climb { get; set; }
        public int Fly { get; set; }
    }
    public class CombatArray
    {
        public int Attack { get; set; }
        public int DifficultyCheck { get; set; }
        public int Damage { get; set; }
        public int Range { get; set; }
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
