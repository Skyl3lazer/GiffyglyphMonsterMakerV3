using System.ComponentModel;
using GiffyglyphMonsterMakerV3.Utility;
using Ganss.Xss;
using Microsoft.AspNetCore.Components;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GiffyglyphMonsterMakerV3.Data
{
    public class Action : Feature
    {
        public Action(string createUserId) : base(createUserId)
        {
            Name = "New Action";
        }
        public override string MarkupDescription(Creature parentCreature)
        {
            string desc =
                "<span class=\"text-white fa-solid p-1 align-middle " + (String.IsNullOrWhiteSpace(CustomIcon) ? Icon : CustomIcon) + " " + RarityStyle + "\"></span></span><span class=\"ms-1\">";

            desc += @"<span class=""fw-bold"">" + Name;
            if (Frequency.Type != FrequencyType.passive)
            {
                desc += " (" + Frequency.StringValue + ")";
            }
            desc += @": </span>";

            //If you want to just totally override a thing, go for it
            if (!string.IsNullOrWhiteSpace(OverrideMarkup))
            {
                var sanitizer = new HtmlSanitizer();
                (new List<string> { "fst-italic", "fw-bold" }).ForEach(item => sanitizer.AllowedClasses.Add(item));
                var html = OverrideMarkup;
                return desc + sanitizer.Sanitize(html);
            }

            desc += (IsSpell ? "<span class=\"fst-italic\">Spell</span>: " + SpellDesc : "");
            desc += "<span class=\"fst-italic\">" + Distance.ToString() + "</span>: ";

            string shapeText = "";
            switch (Shape)
            {
                case TargetShape.line:
                    shapeText += "a " + Range + " ft. line extending from yourself.";
                    break;
                case TargetShape.cone:
                    shapeText += "a " + Range + " ft. cone extending from yourself.";
                    break;
                case TargetShape.emanation:
                    shapeText += "within " + Range + " of yourself.";
                    break;
                case TargetShape.circle:
                    shapeText += "a " + Radius + " ft. circle centered within " + Range + " ft.";
                    break;
                case TargetShape.cube:
                    shapeText += "a " + Radius + " ft. cube centered within " + Range + " ft.";
                    break;
                case TargetShape.square:
                    shapeText += "a " + Radius + " ft. square centered within " + Range + " ft.";
                    break;
                case TargetShape.sphere:
                    shapeText += "a " + Radius + " ft. sphere centered within " + Range + " ft.";
                    break;
                case TargetShape.self:
                    shapeText += "on yourself.";
                    break;
                case TargetShape.target:
                default:
                    shapeText += Targets + " target";
                    if (Targets != 1)
                        shapeText += "s";

                    shapeText += ".";
                    break;
            }

            if (HasSave)
            {
                desc += "DC" + (parentCreature.Offense.DifficultyCheck + parentCreature.Attributes.Dict[RelevantAttribute] + parentCreature.Attributes.AttributeMod) + " vs " + SaveVs + ", ";
            }
            else
            {
                desc += ((parentCreature.Offense.Attack + parentCreature.Attributes.Dict[RelevantAttribute] + parentCreature.Attributes.AttributeMod) >= 0 ? "+" : "") + (parentCreature.Offense.Attack + parentCreature.Attributes.Dict[RelevantAttribute] + parentCreature.Attributes.AttributeMod) + " to hit, ";
            }

            switch (Distance)
            {
                case RangeType.Melee:
                    desc += "reach ";
                    desc += Range + " ft.,";
                    break;
                case RangeType.Ranged:
                default:
                    desc += "range " + Range + " ft.,";
                    break;
            }



            if (MultiAttack > 1)
            {
                desc += " " + MultiAttack + " attacks, ";
            }
            desc += " " + shapeText;

            desc += " <span class=\"fst-italic\">Hit</span>: ";

            if (DealsDamage)
            {
                var dam = (int)Math.Max(Math.Floor((double)(parentCreature.Offense.Damage * DamageMultiplier) / MultiAttack), 1);
                desc += dam + (parentCreature.Offense.RandomizeDamage ? " (" + DiceTools.ConvertToDiceString(parentCreature.Offense.RandomDamageRange, dam) + ")" : "") + " " + ActionDamageType +
                        " damage.";
            }

            if (!String.IsNullOrWhiteSpace(OtherEffect))
                desc += " " + OtherEffect;
            if (!String.IsNullOrWhiteSpace(MissEffect))
                desc += " " + MissEffect;
            desc += "</span>";
            return desc;

        }

        public override string Icon
        {
            get
            {
                if (IsSpell)
                {
                    return "fa-wand-magic-sparkles";
                }
                else if(DealsDamage)
                {
                    switch (Distance)
                    {
                        case RangeType.Melee:
                            return "fa-sword";
                        case RangeType.Ranged:
                            return "fa-bow-arrow";
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else
                {
                    return "fa-caret-right";
                }
            }
        }
        public override FeatureType Type { get; init; } = FeatureType.Action;
        public RangeType Distance { get; set; }
        public int Range { get; set; } = 5;
        public int Radius { get; set; }
        public bool IsSpell { get; set; }
        public string SpellDesc { get; set; } = "";
        public bool DealsDamage { get; set; } = true;
        public int MultiAttack { get; set; } = 1;

        [NotMapped]
        public bool IsMultiAttack
        {
            get => MultiAttack != 1;
            set => MultiAttack = value ? 2 : 1;
        }
        public double DamageMultiplier { get; set; } = 1;
        public string OtherEffect { get; set; } = "";
        public string MissEffect { get; set; } = "";
        public DamageType ActionDamageType { get; set; } = DamageType.bludgeoning;
        public int Targets { get; set; } = 1;
        public TargetShape Shape { get; set; } = TargetShape.target;
        public override void UpdateThisToMatch(Object o)
        {
            if (o is not Action a)
                throw new InvalidDataException("Target is not an action");

            Distance = a.Distance;
            Range = a.Range;
            Radius = a.Radius;
            IsSpell = a.IsSpell;
            SpellDesc = a.SpellDesc;
            DealsDamage = a.DealsDamage;
            MultiAttack = a.MultiAttack;
            DamageMultiplier = a.DamageMultiplier;
            OtherEffect = a.OtherEffect;
            MissEffect = a.MissEffect;
            ActionDamageType = a.ActionDamageType;
            Targets = a.Targets;
            Shape = a.Shape;

            base.UpdateThisToMatch(o);
        }
    }

    public enum DamageType
    {
        acid = 0,
        bludgeoning,
        cold,
        fire,
        force,
        lightning,
        necrotic,
        piercing,
        poison,
        psychic,
        radiant,
        slashing,
        thunder
    }
    public enum TargetShape
    {
        line = 0,
        cone,
        emanation,
        circle,
        target,
        wall,
        square,
        cube,
        sphere,
        self
    }
    public enum RangeType
    {
        Melee = 0,
        Ranged
    }
}
