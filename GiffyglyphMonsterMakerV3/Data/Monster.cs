namespace GiffyglyphMonsterMakerV3.Data
{
    public class Monster : ICreature
    {
        public Monster(string name)
        {
            Name = name;
            ID = Guid.NewGuid();
            Offense = new();
            Defenses = new();
        }
        public Monster()
        {
            Name = "My New Monster";
            ID = Guid.NewGuid();
            Offense = new();
            Defenses = new();
        }
        public Guid ID { get; init; }
        public string Name { get; set; } = "";
        public int CombatLevel { get; set; }
        public Rank MonsterRank { get; set; }
        public Role MonsterRole { get; set; }
        public string MonsterRoleDetail { get; set; }
        public AttributeArray Attributes { get; set; } = new();
        public IEnumerable<IFeature> Features { get; set; } = new List<IFeature>();
        public DefenseArray Defenses { get; set; }
        public int Proficiency { get
            {
                return (int)Math.Floor(1 + (((double)CombatLevel + 3) / 4.0));
            }
        }
        public SpeedArray Speed { get; set; } = new();
        public int SpeedMod { get
            {
                if (MonsterRole == Role.Defender)
                    return -5;
                else if (MonsterRole == Role.Skirmisher)
                    return 5;
                else
                    return 0;
            }
        }
        public OffenseArray Offense { get; set; }
        public int InitiativeModifier { get
            {
                int mod = 0;

                if((MonsterRole == Role.Controller) || (MonsterRole == Role.Supporter)) 
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
        public int ParagonThreat { get; set; }
    }
}