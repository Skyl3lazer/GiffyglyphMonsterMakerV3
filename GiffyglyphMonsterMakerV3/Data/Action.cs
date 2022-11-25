using System.ComponentModel;
using GiffyglyphMonsterMakerV3.Utility;
using Ganss.Xss;
using Microsoft.AspNetCore.Components;
using System;

namespace GiffyglyphMonsterMakerV3.Data
{
    public class Action : IFeature
    {
        public string Name { get; set; } = "";

        public string MarkupDescription
        {
            get
            {
                //If you want to just totally override a thing, go for it
                if (!string.IsNullOrWhiteSpace(OverrideMarkup))
                {
                    var sanitizer = new HtmlSanitizer();
                    (new List<string>{"fst-italic", "fw-bold"}).ForEach(item => sanitizer.AllowedClasses.Add(item));
                    var html = OverrideMarkup;
                    return sanitizer.Sanitize(html);
                }

                string desc = @"<span class=""fw-bold"">" + Name;
                if (Frequency.Type != FrequencyType.passive)
                {
                    desc += " (" + Frequency.StringValue + ")";
                }
                desc += @": </span>";

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
                    case TargetShape.target:
                    default:
                        if (Targets > 0)
                        {
                            shapeText += Targets + " target";
                            if (Targets > 1)
                                shapeText += "s";
                        }
                        shapeText += ".";
                        break;
                }

                if (HasSave)
                {
                    desc += "DC" + (Parent.Offense.DifficultyCheck + Parent.Attributes.HighStat + Parent.Attributes.AttributeMod) + " vs " + SaveVs + ", ";
                }
                else
                {
                    desc += "+" + (Parent.Offense.Attack + Parent.Attributes.HighStat + Parent.Attributes.AttributeMod) + " to hit, ";
                }

                switch (Distance)
                {
                    case RangeType.Melee:
                        desc += "reach ";
                        desc += Range+" ft.,";
                        break;
                    case RangeType.Ranged:
                    default:
                        desc += "range " + Range + " ft.,";
                        break;
                }

                

                if (MultiAttack > 1)
                {
                    desc += " "+MultiAttack + " attacks, ";
                }
                desc += " " + shapeText;

                desc += " <span class=\"fst-italic\">Hit</span>: ";

                if (DealsDamage)
                {
                    var dam = (int)Math.Max(Math.Floor((double)(Parent.Offense.Damage * DamageMultiplier) / MultiAttack), 1);
                    desc += dam + (Parent.Offense.RandomizeDamage ? " (" + DiceTools.ConvertToDiceString(Parent.Offense.RandomDamageRange, dam)+")" : "") + " " + ActionDamageType +
                            " damage.";
                }

                if (!String.IsNullOrWhiteSpace(OtherEffect))
                    desc += " " + OtherEffect;
                if (!String.IsNullOrWhiteSpace(MissEffect))
                    desc += " " + MissEffect;
                return desc;
            }
        }

        public Guid Id { get; init; } = Guid.NewGuid();
        private ICreature _parent;
        public ICreature Parent
        {
            get
            {
                return _parent;
            }
            init
            {
                _parent = value;
                _parent.Offense.PropertyChanged += UpdateOffenses;
                _parent.Attributes.PropertyChanged += UpdateAttributes;
            }
        }

        private void UpdateAttributes(object? sender, PropertyChangedEventArgs e)
        {
            _parent.Attributes = (AttributeArray)sender;
        }

        private void UpdateOffenses(object? sender, PropertyChangedEventArgs e)
        {
            _parent.Offense = (OffenseArray)sender;
        }
        public string OverrideMarkup { get; set; }
        public virtual FeatureType Type { get; init; } = FeatureType.Action;
        public RangeType Distance { get; set; }
        public bool HasSave { get; set; }
        public string SaveVs { get; set; }

        public int Range { get; set; }
        public int Radius { get; set; }
        public RarityType Rarity { get; set; }
        public string Icon { get; set; }
        public bool IsSpell { get; set; }
        public string SpellDesc { get; set; }
        public bool DealsDamage { get; set; }
        public int MultiAttack { get; set; } = 1;
        public double DamageMultiplier { get; set; } = 1;
        public string OtherEffect { get; set; }
        public string MissEffect { get; set; }
        public DamageType ActionDamageType { get; set; }
        public FeatureFrequency Frequency { get; set; } = new();
        public int Targets { get; set; } = 0;
        public TargetShape Shape { get; set; }
        public string RarityStyle
        {
            get
            {
                string style = "";
                switch (Rarity)
                {
                    case RarityType.Common:
                        style += "ability-common rounded-circle";
                        break;
                    case RarityType.Uncommon:
                        style += "ability-uncommon rounded-end rounded-start-bottom";
                        break;
                    case RarityType.Rare:
                        style += "ability-rare rounded-end-top rounded-start-bottom";
                        break;
                    default:
                        style += "";
                        break;
                }
                return style;
            }
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
        wall
    }
    public enum RangeType
    {
        Melee = 0,
        Ranged
    }
}
