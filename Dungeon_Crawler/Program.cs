using Dungeon_Crawler.Game;

var log = new CombatLog();
Console.CursorVisible = false;

var level = new LevelData();
level.Load("Levels/Level1.txt");

var player = new Player(level.PlayerStart.x, level.PlayerStart.y);

int turn = 0;
bool running = true;

while (running)
{
    Console.Clear();

    foreach (var e in level.Elements)
        e.Draw();

    player.Draw();

    Console.SetCursorPosition(0, level.Height + 1);
    Console.WriteLine($"Player HP: {player.HP} Turn: {turn}");
    Console.WriteLine("Tryck ESC för att avsluta");
    log.Draw(level.Height + 3);

    foreach (var enemy in level.Enemies)
        enemy.Update(level, player);

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
    turn++;
}
