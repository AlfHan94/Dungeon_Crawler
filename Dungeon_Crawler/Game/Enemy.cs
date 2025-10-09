using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler.Game
{
    abstract class Enemy : LevelElement
    {
        public string Name { get; protected set; }
        public int HP { get; protected set; }
        public Dice AttackDice { get; protected set; }
        public Dice DefenceDice { get; protected set; }

        public bool IsDead => HP <= 0;
        public abstract void Update(LevelData level, Player player);
        public void TakeDamage(int amount)
        {
            HP -= amount;
            if (HP <= 0)
            {
                HP = 0;
            }
        }
    }

}
