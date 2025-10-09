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
            double distance = Math.Sqrt(Math.Pow(X - player.X, 2) + Math.Pow(Y - player.Y, 2));

            if (distance <= 2)
            {
                int dx = 0, dy = 0;

                if (player.X > X) dx = -1;
                if (player.X < X) dx = 1;
                if (player.Y > Y) dy = -1;
                if (player.Y < Y) dy = 1;

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
}
