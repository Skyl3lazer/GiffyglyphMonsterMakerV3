namespace GiffyglyphMonsterMakerV3.Utility
{
    public static class DiceTools
    {
        static Dictionary<DamageRange, Dice> rangeConversion = new Dictionary<DamageRange, Dice>();

        static DiceTools()
        {
            rangeConversion.Add(DamageRange.Accurate, new Dice(){Average = 2.5, Name = "d4"});
            rangeConversion.Add(DamageRange.Predictable, new Dice() { Average = 3.5, Name = "d6" });
            rangeConversion.Add(DamageRange.Reliable, new Dice() { Average = 4.5, Name = "d8" });
            rangeConversion.Add(DamageRange.Irregular, new Dice() { Average = 5.5, Name = "d10" });
            rangeConversion.Add(DamageRange.Volatile, new Dice() { Average = 6.5, Name = "d12" });
            rangeConversion.Add(DamageRange.Wild, new Dice() { Average = 10.5, Name = "d20" });
            rangeConversion.Add(DamageRange.Chaotic, new Dice() { Average = 55.5, Name = "d100" });
        }
        public static Roll ConvertToDice(DamageRange range, double average)
        {
            Roll roll = new Roll();
            double numDie = average / rangeConversion[range].Average;
            roll.Die = rangeConversion[range];
            roll.Number = (int)Math.Max(Math.Round(numDie), 1);
            double averageRoll = roll.Number * roll.Die.Average;
            roll.Mod = (int)Math.Round(average - averageRoll);
            return roll;
        }

        public static string ConvertToDiceString(DamageRange range, double average)
        {
            var roll = DiceTools.ConvertToDice(range, average);
            string rollText = roll.Number + roll.Die.Name;
            if (roll.Mod > 0)
            {
                rollText += "+" + roll.Mod;
            }
            else if (roll.Mod < 0)
            {
                rollText += roll.Mod;
            }
            return rollText;
        }
    }

    public enum DamageRange
    {
        Accurate,
        Predictable,
        Reliable,
        Irregular,
        Volatile,
        Wild,
        Chaotic
    }

    public class Dice
    {
        public double Average { get; set; }
        public string Name { get; set; }
    }

    public class Roll
    {
        public Dice Die { get; set; }
        public int Number { get; set; }
        public int Mod { get; set; }
    }
}
