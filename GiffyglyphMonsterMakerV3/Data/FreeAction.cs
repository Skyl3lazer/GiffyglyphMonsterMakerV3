using Ganss.Xss;

namespace GiffyglyphMonsterMakerV3.Data
{
    public class FreeAction : Action
    {
        public FreeAction(string createUserId) : base(createUserId)
        {
            Name = "New Free Action";
            Type = FeatureType.Free;
        }
    }
}
