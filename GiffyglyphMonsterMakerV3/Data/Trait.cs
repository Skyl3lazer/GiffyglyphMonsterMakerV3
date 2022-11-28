using Ganss.Xss;

namespace GiffyglyphMonsterMakerV3.Data
{
    public class Trait : Feature
    {
        public Trait(string createUserId) : base(createUserId)
        {
            Name = "New Trait";
            Type = FeatureType.Trait;
        }
        public override string MarkupDescription(Creature parentCreature)
        {
                //If you want to just totally override a thing, go for it
                if (!string.IsNullOrWhiteSpace(OverrideMarkup))
                {
                    var sanitizer = new HtmlSanitizer();
                    (new List<string> { "fst-italic", "fw-bold" }).ForEach(item => sanitizer.AllowedClasses.Add(item));
                    var html = OverrideMarkup;
                    return sanitizer.Sanitize(html);
                }

                string desc =
                    "<span class=\"text-white fa-solid p-1 " + (String.IsNullOrWhiteSpace(CustomIcon) ? Icon : CustomIcon) + " " + RarityStyle + "\"></span></span><span class=\"ms-1\">";
                desc += @"<span class=""fw-bold"">" + Name;
                desc += "</span>";

                return desc;
        }
        public override void UpdateThisToMatch(Object o)
        {
            if (o is not Trait a)
                throw new InvalidDataException("Target is not a Trait");
            base.UpdateThisToMatch(o);
        }
    }
}
