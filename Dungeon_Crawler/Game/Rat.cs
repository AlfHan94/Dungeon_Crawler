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
            if (HP <= 0)
                return;
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
                    Game.log.AddColored($"{attackText}, and dealt {damage} damage!", ConsoleColor.Red);
                }
                else
                {
                    Game.log.AddColored($"{attackText}, but did not manage to make any damage.", ConsoleColor.Green);
                }

                if (player.HP > 0)
                {
                    int counterAttack = player.AttackDice.Throw();
                    int enemyDef = DefenceDice.Throw();
                    int counterDamage = counterAttack - enemyDef;

                    string counterText = $"You (ATK: {player.AttackDice} => {counterAttack}) counterattacked the {Name} (DEF: {DefenceDice} => {enemyDef})";

                    if (counterDamage > 0)
                    {
                        TakeDamage(counterDamage);

                        if (HP <= 0)
                        {
                            Game.log.AddColored($"{counterText}, and dealt {counterDamage} damage — killing the {Name}!", ConsoleColor.Yellow);
                        }
                        else
                        {
                            Game.log.AddColored($"{counterText}, and dealt {counterDamage} damage!", ConsoleColor.DarkYellow);
                        }
                    }
                    else
                    {
                        Game.log.AddColored($"{counterText}, but did not manage to make any damage.", ConsoleColor.DarkYellow);
                    }
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
