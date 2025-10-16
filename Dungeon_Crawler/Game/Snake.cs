using System;

namespace Dungeon_Crawler.Game
{
    class Snake : Enemy
    {
        public Snake(int x, int y)
        {
            X = x;
            Y = y;
            Name = "Snake";
            HP = 25;
            Symbol = 's';
            Color = ConsoleColor.Green;
            AttackDice = new Dice(3, 4, 2);
            DefenceDice = new Dice(1, 8, 5);
        }
        public override void Update(LevelData level, Player player)
        {
            if (HP <= 0)
                return;

            double distance = Math.Sqrt(Math.Pow(X - player.X, 2) + Math.Pow(Y - player.Y, 2));

            if (distance > 2)
                return;

            int dx = 0, dy = 0;

            if (player.X > X) dx = -1;
            if (player.X < X) dx = 1;
            if (player.Y > Y) dy = -1;
            if (player.Y < Y) dy = 1;

            int newX = X + dx;
            int newY = Y + dy;

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

            if (newX == player.X && newY == player.Y)
            {
                int attackRoll = AttackDice.Throw();
                int defenceRoll = player.DefenceDice.Throw();
                int damage = attackRoll - defenceRoll;

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
                            Game.log.AddColored($"{counterText}, and dealt {counterDamage} damage — killing the {Name}!", ConsoleColor.Yellow);
                        else
                            Game.log.AddColored($"{counterText}, and dealt {counterDamage} damage!", ConsoleColor.DarkYellow);
                    }
                    else
                    {
                        Game.log.AddColored($"{counterText}, but did not manage to make any damage.", ConsoleColor.DarkYellow);
                    }
                }
            }
        }
    }
}
