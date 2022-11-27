using Ganss.Xss;

namespace GiffyglyphMonsterMakerV3.Data
{
    public class Reaction : Feature
    {
        public Reaction()
        {
            Name = "New Reaction";
            Type = FeatureType.Reaction;
        }
        public override string MarkupDescription
        {
            get
            {
                //If you want to just totally override a thing, go for it
                if (!string.IsNullOrWhiteSpace(OverrideMarkup))
                {
                    var sanitizer = new HtmlSanitizer();
                    (new List<string> { "fst-italic", "fw-bold" }).ForEach(item => sanitizer.AllowedClasses.Add(item));
                    var html = OverrideMarkup;
                    return sanitizer.Sanitize(html);
                }

                string desc = "";

                return desc;
            }
        }
    }
}
