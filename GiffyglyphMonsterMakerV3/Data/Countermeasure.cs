﻿using System.ComponentModel.DataAnnotations.Schema;
using Ganss.Xss;

namespace GiffyglyphMonsterMakerV3.Data
{
    public class Countermeasure : Feature
    {
        public Countermeasure(string createUserId) : base(createUserId)
        {
            Name = "New Countermeasure";
            Type = FeatureType.Countermeasure;
        }

        public override string MarkupDescription(Creature parentCreature)
        {
            string desc =
                "<span class=\"text-white fa-solid p-1 align-middle " + (String.IsNullOrWhiteSpace(CustomIcon) ? Icon : CustomIcon) + " " + RarityStyle + "\"></span></span><span class=\"ms-1\">";
            desc += @"<span class=""fw-bold"">" + Name;
            desc += ":</span> ";

            //If you want to just totally override a thing, go for it
            if (!string.IsNullOrWhiteSpace(OverrideMarkup))
            {
                var sanitizer = new HtmlSanitizer();
                (new List<string> { "fst-italic", "fw-bold" }).ForEach(item => sanitizer.AllowedClasses.Add(item));
                var html = OverrideMarkup;
                return desc + sanitizer.Sanitize(html);
            }

            desc += CountermeasureDescription;

            return desc;

        }
        public override void UpdateThisToMatch(Object o)
        {
            if (o is not Countermeasure a)
                throw new InvalidDataException("Target is not a Countermeasure");

            base.UpdateThisToMatch(o);
        }
        public override string Icon
        {
            get
            {
                return "fa-skull";
            }
        }
        public string CountermeasureDescription { get; set; }
    }
}
