using System;

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

            int newX = X + dx;
            int newY = Y + dy;

            if (newX == player.X && newY == player.Y)
            {
                int attackRoll = AttackDice.Throw();
                int defenceRoll = player.DefenceDice.Throw();
                int damage = attackRoll - defenceRoll;

                string attackText = $"The {Name} (ATK: {AttackDice} => {attackRoll}) attacked you (DEF: {player.DefenceDice} => {defenceRoll})";

                if (damage > 0)
                {
                    player.TakeDamage(damage);
                    Program.log.AddColored($"{attackText}, and dealt {damage} damage!", ConsoleColor.Red);
                }
                else
                {
                    Program.log.AddColored($"{attackText}, but did not manage to make any damage.", ConsoleColor.Green);
                }

                if (player.HP > 0)
                {
                    int counterAttack = player.AttackDice.Throw();
                    int enemyDef = DefenceDice.Throw();
                    int counterDamage = counterAttack - enemyDef;

                    string counterText = $"You (ATK: {player.AttackDice} => {counterAttack} counterattacked the {Name} (DEF: {defenceRoll} => {enemyDef})";

                    if (counterDamage > 0)
                    {
                        TakeDamage(counterDamage);
                        Program.log.AddColored($"{counterText}, and dealt {counterDamage} damage!", ConsoleColor.DarkYellow);
                    }
                    else
                    {
                        Program.log.AddColored($"{counterText}, but did not manage to make any damage.", ConsoleColor.DarkYellow);
                    }

                    if (HP <= 0)
                        Program.log.AddColored($"{Name} has been defeated!", ConsoleColor.Yellow);
                }


                return; 
            }

            
            bool blocked = false;
            foreach (var e in level.Elements)
            {
                if (e.X == newX && e.Y == newY && (e is Wall || e is Enemy))
                {
                    blocked = true;
                    break;
                }
            }


            if (!blocked)
            {
                X = newX;
                Y = newY;
            }
        }
    }
}
