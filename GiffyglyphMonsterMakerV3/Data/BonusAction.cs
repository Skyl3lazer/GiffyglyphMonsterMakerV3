using Microsoft.EntityFrameworkCore;

namespace GiffyglyphMonsterMakerV3.Data
{
    public class BonusAction : Action
    {
        public BonusAction(string createUserId) : base(createUserId)
        {
            Name = "New Bonus Action";
            Type = FeatureType.Bonus;
        }

        public override void UpdateThisToMatch(Object o)
        {
            if (o is not BonusAction a)
                throw new InvalidDataException("Target is not a Bonus Action");

            base.UpdateThisToMatch(o);
        }
    }
}
