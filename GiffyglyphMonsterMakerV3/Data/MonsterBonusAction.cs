using Microsoft.EntityFrameworkCore;

namespace GiffyglyphMonsterMakerV3.Data
{
    public class MonsterBonusAction : MonsterAction
    {
        public override FeatureType Type { get; init; } = FeatureType.Bonus;
    }
}
