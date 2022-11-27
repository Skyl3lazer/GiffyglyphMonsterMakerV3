using Ganss.Xss;

namespace GiffyglyphMonsterMakerV3.Data
{
    public class FreeAction : Action
    {
        public FreeAction()
        {
            Name = "New Free Action";
            Type = FeatureType.Free;
        }
        public override void UpdateThisToMatch(Object o)
        {
            if (o is not FreeAction a)
                throw new InvalidDataException("Target is not a Free Action");

            base.UpdateThisToMatch(o);
        }
    }
}
