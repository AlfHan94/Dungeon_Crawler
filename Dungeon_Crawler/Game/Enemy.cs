using System;

namespace Dungeon_Crawler.Game
{
    abstract class Enemy : LevelElement
    {
        public bool IsDead => HP <= 0;
        public string Name { get; protected set; }
        public int HP { get; protected set; }
        public Dice AttackDice { get; protected set; }
        public Dice DefenceDice { get; protected set; }
        public abstract void Update(LevelData level, Player player);
        public void TakeDamage(int amount)
        {
            HP -= amount;
            if (HP < 0)
            {
                HP = 0;
            }
        }
        public override void Draw()
        {
            if (HP <= 0)
                return; 
            base.Draw();
        }
    }
}
