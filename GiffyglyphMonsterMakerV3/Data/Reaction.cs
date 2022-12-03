using Ganss.Xss;
using System.ComponentModel.DataAnnotations.Schema;

namespace GiffyglyphMonsterMakerV3.Data
{
    [Table("Features")]
    public class Reaction : Action
    {
        public Reaction(string createUserId) : base(createUserId)
        {
            Name = "New Reaction";
            Type = FeatureType.Reaction;
        }
    }
}
