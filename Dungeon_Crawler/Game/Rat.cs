using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler.Game
{
    class Rat : Enemy
    {
        private static Random rng = new Random();
        public Rat(int x, int y)
        {
            X = x;
            Y = y;
            Name = "Rat";
            HP = 10;
            Symbol = 'r';
            Color = ConsoleColor.Yellow;
            AttackDice = new Dice(1, 6, 3);
            DefenceDice = new Dice(1, 6, 1);
        }
        public override void Update(LevelData level, Player player)
        {
            int dx = 0, dy = 0;

            switch (rng.Next(4))
            {
                case 0: dx = 1; break;
                case 1: dx = -1; break;
                case 2: dy = 1; break;
                case 3: dy = -1; break;
            }
            bool blocked = false;
            foreach (var e in level.Elements)
            {
                if (e.X == X + dx && e.Y == Y + dy && e is Wall)
                {
                    blocked = true;
                    break;
                }
            }
            if (!blocked)
            {
                X += dx;
                Y += dy;
            }
        }
    }
}
