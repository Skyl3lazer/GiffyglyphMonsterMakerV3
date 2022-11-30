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
        public override void UpdateThisToMatch(Object o)
        {
            if (o is not Reaction a)
                throw new InvalidDataException("Target is not a Reaction");

            base.UpdateThisToMatch(o);
        }
    }
}
