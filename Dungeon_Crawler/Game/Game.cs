using System;
using System.Threading;
using System.Linq;

namespace Dungeon_Crawler.Game
{
    class Game
    {
        public static CombatLog log = new CombatLog();

        public static void Run()
        {
            Console.CursorVisible = false;

            var level = new LevelData();
            level.Load("Levels/Level1.txt");
            var player = new Player(level.PlayerStart.x, level.PlayerStart.y);
            int turn = 0;
            bool running = true;

            while (running)
            {
                Console.Clear();
                level.UpdateVisibility(player, 5);

                foreach (var e in level.Elements)
                    e.Draw();

                player.Visible = true;
                player.Draw();

                Console.SetCursorPosition(0, level.Height + 1);
                Console.WriteLine($"Player HP: {player.HP}   Turn: {turn}   Enemies left: {level.Enemies.Count}");
                Console.WriteLine("Press ESC to exit...");

                log.Draw(level.Height + 3);

                var key = Console.ReadKey(true).Key;
                int dx = 0, dy = 0;

                switch (key)
                {
                    case ConsoleKey.UpArrow: dy = -1; break;
                    case ConsoleKey.DownArrow: dy = 1; break;
                    case ConsoleKey.LeftArrow: dx = -1; break;
                    case ConsoleKey.RightArrow: dx = 1; break;
                    case ConsoleKey.Escape: running = false; break;
                }

                if (dx != 0 || dy != 0)
                {
                    int newX = player.X + dx;
                    int newY = player.Y + dy;

                    bool blocked = false;
                    foreach (var e in level.Elements)
                    {
                        if (e.X == newX && e.Y == newY && e is Wall)
                        {
                            blocked = true;
                            break;
                        }
                    }

                    if (!blocked)
                    {
                        Enemy enemyThere = null;
                        foreach (var enemy in level.Enemies)
                        {
                            if (enemy.X == newX && enemy.Y == newY)
                            {
                                enemyThere = enemy;
                                break;
                            }
                        }

                        if (enemyThere != null)
                        {
                            player.Attack(enemyThere, log);

                            if (enemyThere.IsDead)
                            {
                                level.Elements.Remove(enemyThere);
                                level.Enemies.Remove(enemyThere);
                            }
                        }
                        else
                        {
                            player.Move(dx, dy);
                        }
                    }
                }
                foreach (var enemy in level.Enemies.ToList())
                {
                    if (!enemy.IsDead)
                        enemy.Update(level, player);
                }
                foreach (var dead in level.Enemies.Where(e => e.IsDead).ToList())
                {
                    level.Enemies.Remove(dead);
                    level.Elements.Remove(dead);
                }

                if (player.HP <= 0)
                {
                    Console.Clear();
                    string text = "GAME OVER!";
                    int x = (Console.WindowWidth - text.Length) / 2;
                    int y = Console.WindowHeight / 2;

                    for (int i = 0; i < 10; i++)
                    {
                        Console.ForegroundColor = (i % 2 == 0) ? ConsoleColor.Red : ConsoleColor.DarkRed;
                        Console.SetCursorPosition(x, y);
                        Console.Write(text);
                        Thread.Sleep(300);
                    }
                    Console.ResetColor();
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(x, y);
                    Console.WriteLine(text);
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.SetCursorPosition((Console.WindowWidth - 24) / 2, y + 2);
                    Console.WriteLine("Press any key to exit...");
                    Console.ResetColor();
                    Console.ReadKey(true);
                    running = false;
                    continue;
                }

                turn++;
            }
        }
    }
}
