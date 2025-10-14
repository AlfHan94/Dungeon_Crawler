using System;

namespace Dungeon_Crawler.Game
{
    abstract class LevelElement
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Symbol { get; set; }
        public ConsoleColor Color { get; set; }

        public bool Visible { get; set; } = false;
        public bool Seen { get; set; } = false;


        public virtual void Draw()
        {
            if (!Visible && !(Seen && this is Wall))
                return;

            if (this is Enemy enemy && enemy.IsDead)
                return;

            Console.ForegroundColor = Color;
            Console.SetCursorPosition(X, Y);
            Console.Write(Symbol);
            Console.ResetColor();
        }
    }
}
