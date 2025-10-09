using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler.Game
{
    class Dice
    {
        private static Random rng = new Random();

        private int numberOfDice;
        private int sidesPerDice;
        private int modifier;

        public Dice(int numberOfDice, int sidesPerDie, int modifier)
        {
            this.numberOfDice = numberOfDice;
            this.sidesPerDice = sidesPerDie;
            this.modifier = modifier;
        }
        public int Throw()
        {
            int total = 0;
            for (int i = 0; i < numberOfDice; i++)
            {
                total += rng.Next(1, sidesPerDice + 1);
            }
            total += modifier;
            return total;
        }
        public override string ToString()
        {
            string sign = modifier >= 0 ? "+" : "-";
            return $"{numberOfDice}d{sidesPerDice}{sign}{Math.Abs(modifier)}";
        }
    }
}
