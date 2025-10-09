using System;

namespace Dungeon_Crawler.Game
{
    class Player : LevelElement
    {
        public Player(int x, int y)
        {
            X = x;
            Y = y;
            Symbol = '@';
            Color = ConsoleColor.Blue;
        }

        public void Move(int dx, int dy)
        {
            X += dx;
            Y += dy;
        }
    }
}
