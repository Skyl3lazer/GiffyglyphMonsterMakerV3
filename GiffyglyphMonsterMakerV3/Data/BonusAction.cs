using Microsoft.EntityFrameworkCore;

namespace GiffyglyphMonsterMakerV3.Data
{
    public class BonusAction : Action
    {
        public override FeatureType Type { get; init; } = FeatureType.Bonus;
    }
}
