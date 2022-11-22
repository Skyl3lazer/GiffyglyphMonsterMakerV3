namespace GiffyglyphMonsterMakerV3.Data
{
    public class Monster : ICreature
    {
        public Monster(string name)
        {
            Name = name;
            ID = Guid.NewGuid();
        }
        public Monster()
        {
            Name = "My New Monster";
            ID = Guid.NewGuid();
        }

        public Guid ID { get; init; }
        public string Name { get; set; } = "";
        public int CombatLevel { get; set; }
        public Rank MonsterRank { get; set; }
        public Role MonsterRole { get; set; }
        public string MonsterRoleDetail { get; set; }
        public AttributeArray Attributes { get; set; } = new();
        public IEnumerable<IFeature> Features { get; set; } = new List<IFeature>();
        public DefenseArray Defenses { get; set; } = new();
        public int Proficiency { get; set; }
        public SpeedArray Speed { get; set; } = new();
        public CombatArray Combat { get; set; } = new();
        public int InitiativeModifier { get; set; }
        public SizeType Size { get; set; }
        public CreatureType Type { get; set; }
        public string TypeDetail { get; set; }
    }
}