using System;

namespace Dungeon_Crawler.Game
{
    class Player : LevelElement
    {
        public int HP { get; private set; }
        public Dice AttackDice { get; private set; }
        public Dice DefenceDice { get; private set; }
        public string Name { get; private set; }

        public Player(int x, int y)
        {
            X = x;
            Y = y;
            Symbol = '@';
            Color = ConsoleColor.Blue;
            Name = "Player";
            HP = 100;
            AttackDice = new Dice(2, 6, 2);
            DefenceDice = new Dice(2, 6, 0);
        }

        public void Move(int dx, int dy)
        {
            X += dx;
            Y += dy;
        }

        public void Attack(Enemy enemy, CombatLog log)
        {
            // --- Spelarens attack ---
            int attackRoll = AttackDice.Throw();
            int defenceRoll = enemy.DefenceDice.Throw();
            int damage = attackRoll - defenceRoll;

            string playerAttackText = $"You (ATK: {AttackDice} => {attackRoll}) attacked the {enemy.Name} (DEF: {enemy.DefenceDice} => {defenceRoll})";

            if (damage > 0)
            {
                enemy.TakeDamage(damage);
                log.AddColored($"{playerAttackText}, and dealt {damage} damage!", ConsoleColor.DarkYellow);
            }
            else
            {
                log.AddColored($"{playerAttackText}, but did not manage to make any damage.", ConsoleColor.DarkYellow);
            }

            // --- Fiendens motattack ---
            if (enemy.HP > 0)
            {
                int counterAttack = enemy.AttackDice.Throw();
                int playerDef = DefenceDice.Throw();
                int counterDamage = counterAttack - playerDef;

                string enemyAttackText = $"The {enemy.Name} (ATK: {enemy.AttackDice} => {counterAttack}) attacked you (DEF: {DefenceDice} => {playerDef})";

                if (counterDamage > 0)
                {
                    HP -= counterDamage;
                    log.AddColored($"{enemyAttackText}, and dealt {counterDamage} damage!", ConsoleColor.Red);
                }
                else
                {
                    log.AddColored($"{enemyAttackText}, but did not manage to make any damage.", ConsoleColor.Green);
                }
            }

            if (enemy.HP <= 0)
            {
                log.AddColored($"{enemy.Name} has been defeated!", ConsoleColor.Yellow);
            }
        }



    }
}
