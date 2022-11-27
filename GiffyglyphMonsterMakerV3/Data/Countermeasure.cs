﻿using Ganss.Xss;

namespace GiffyglyphMonsterMakerV3.Data
{
    public class Countermeasure : Feature
    {
        public Countermeasure()
        {
            Name = "New Countermeasure";
            Type = FeatureType.Countermeasure;
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

                string desc =
                    "<span class=\"text-white fa-solid p-1 " + (String.IsNullOrWhiteSpace(CustomIcon) ? Icon : CustomIcon) + " " + RarityStyle + "\"></span></span><span class=\"ms-1\">";
                desc += @"<span class=""fw-bold"">" + Name;
                desc += "</span>";
                return desc;
            }
        }
        public override void UpdateThisToMatch(Object o)
        {
            if (o is not Countermeasure a)
                throw new InvalidDataException("Target is not a Countermeasure");

            base.UpdateThisToMatch(o);
        }
    }
}
