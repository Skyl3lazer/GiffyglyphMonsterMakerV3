using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace GiffyglyphMonsterMakerV3.Data;

public class Monster : Creature
{
    public Monster(string name, string createUserId) : base(createUserId)
    {
        Name = name;
        Id = Guid.NewGuid();
        Offense = new OffenseArray();
        Defenses = new DefenseArray();
        OtherSpeeds = new Dictionary<MovementType, int>();
        Senses = new Dictionary<SenseType, int>();
        Languages = new List<string>();
        Items = new List<string>();
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