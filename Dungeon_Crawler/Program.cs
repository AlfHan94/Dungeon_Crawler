using Dungeon_Crawler.Game;

Console.CursorVisible = false;

var level = new LevelData();
level.Load("Levels/Level1.txt");

var player = new Player(level.PlayerStart.x, level.PlayerStart.y);

bool running = true;

while (running)
{
    Console.Clear();

    foreach (var e in level.Elements)
        e.Draw();

    player.Draw();

    Console.SetCursorPosition(0, level.Height + 1);
    Console.WriteLine($"Player: ({player.X}, {player.Y})");
    Console.WriteLine("Tryck ESC för att avsluta");

    foreach (var enemy in level.Enemies)
        enemy.Update(level, player);

    var key = Console.ReadKey(true).Key;

    switch (key)
    {
        case ConsoleKey.UpArrow:
            player.Move(0, -1);
            break;
        case ConsoleKey.DownArrow:
            player.Move(0, 1);
            break;
        case ConsoleKey.LeftArrow:
            player.Move(-1, 0);
            break;
        case ConsoleKey.RightArrow:
            player.Move(1, 0);
            break;
        case ConsoleKey.Escape:
            running = false;
            break;
    }
}
