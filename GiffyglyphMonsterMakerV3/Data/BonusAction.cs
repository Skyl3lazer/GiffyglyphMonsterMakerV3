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
        
    }
}
