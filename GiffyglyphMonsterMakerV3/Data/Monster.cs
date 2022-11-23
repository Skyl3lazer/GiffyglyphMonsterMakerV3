namespace GiffyglyphMonsterMakerV3.Data;

public class Monster : ICreature
{
    public Monster(string name)
    {
        Name = name;
        ID = Guid.NewGuid();
        Offense = new OffenseArray();
        Defenses = new DefenseArray();
        OtherSpeeds = new Dictionary<MovementType, int>();
        Senses = new Dictionary<SenseType, int>();
        Languages = new List<string>();
        Items = new List<string>();
        Features = new List<IFeature>();
        Features.Add(new Action() { Name = "Hit Them", Rarity = RarityType.Common, Description = "one target", Range = 5, Distance = RangeType.Melee, Icon = "fa-sword", Type = FeatureType.Action});
    }

    public Monster()
    {
        Name = "My New Monster";
        ID = Guid.NewGuid();
        Offense = new OffenseArray();
        Defenses = new DefenseArray();
        OtherSpeeds = new Dictionary<MovementType, int>();
        Senses = new Dictionary<SenseType, int>();
        Languages = new List<string>();
        Items = new List<string>();
        Features = new List<IFeature>(); 
        Features.Add(new Action() { Name = "Hit Them", Rarity = RarityType.Common, Description = "one target", Range = 5, Distance = RangeType.Melee, Icon = "fa-sword", Type = FeatureType.Action });
    }

    public int ParagonThreat { get; set; }

    public int MaxRange
    {
        get
        {
            var rangedFeatures = Features.Where(a =>
                a.Type == FeatureType.Action &&
                ((Action)a).Distance == RangeType.Ranged).Select(b => (Action)b);
            rangedFeatures.OrderByDescending(b => b.Distance);
            return rangedFeatures.FirstOrDefault(new Action { Range = 0 }).Range;
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
            return meleeFeatures.FirstOrDefault(new Action { Range = 5 }).Range;
        }
    }

    public Guid ID { get; init; }
    public string Name { get; set; } = "";
    public int CombatLevel { get; set; }
    public Rank MonsterRank { get; set; }
    public Role MonsterRole { get; set; }
    public string MonsterRoleDetail { get; set; }
    public AttributeArray Attributes { get; set; } = new();
    public List<IFeature> Features { get; set; }
    public DefenseArray Defenses { get; set; }

    public int Proficiency => (int)Math.Floor(1 + ((double)CombatLevel + 3) / 4.0);

    public int WalkSpeed { get; set; }
    public Dictionary<MovementType, int> OtherSpeeds { get; set; }

    public int SpeedMod
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

    public OffenseArray Offense { get; set; }

    public int InitiativeModifier
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

    public SizeType Size { get; set; }
    public CreatureType Type { get; set; }
    public string TypeDetail { get; set; }
    public Dictionary<SenseType, int> Senses { get; set; }
    public List<string> Languages { get; set; }
    public List<string> Items { get; set; }
}