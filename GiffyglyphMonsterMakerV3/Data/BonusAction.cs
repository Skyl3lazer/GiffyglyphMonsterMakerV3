using Microsoft.EntityFrameworkCore;

namespace GiffyglyphMonsterMakerV3.Data
{
    public class BonusAction : Action
    {
        public BonusAction()
        {
            Name = "New Bonus Action";
        }
        public override FeatureType Type { get; init; } = FeatureType.Bonus;

        public override void UpdateThisToMatch(Object o)
        {
            if (o is not BonusAction a)
                throw new InvalidDataException("Target is not a Bonus Action");

            base.UpdateThisToMatch(o);
        }
    }
}
