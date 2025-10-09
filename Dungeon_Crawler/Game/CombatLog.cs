using System;
using System.Collections.Generic;

namespace Dungeon_Crawler.Game
{
    class CombatLog
    {
        private List<string> entries = new List<string>();
        private List<ConsoleColor> colors = new List<ConsoleColor>();
        private int maxEntries = 5; // Visa senaste 5 raderna

        public void Add(string message)
        {
            AddColored(message, ConsoleColor.White);
        }

        public void AddColored(string message, ConsoleColor color)
        {
            entries.Add(message);
            colors.Add(color);

            if (entries.Count > maxEntries)
            {
                entries.RemoveAt(0);
                colors.RemoveAt(0);
            }
        }

        public void Draw(int startY)
        {
            // Säkerställ att vi inte skriver utanför fönstret
            if (startY >= Console.BufferHeight - (maxEntries + 2))
                startY = Console.BufferHeight - (maxEntries + 2);

            // Rensa gammal text under loggen
            for (int i = 0; i < maxEntries + 2; i++)
            {
                if (startY + i < Console.BufferHeight)
                {
                    Console.SetCursorPosition(0, startY + i);
                    Console.Write(new string(' ', Console.WindowWidth - 1));
                }
            }

            // Rita rubriken
            Console.SetCursorPosition(0, startY);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("=== Combat Log ===");

            // Rita meddelanden med färg
            int y = startY + 1;
            for (int i = 0; i < entries.Count; i++)
            {
                if (y < Console.BufferHeight)
                {
                    Console.SetCursorPosition(0, y++);
                    Console.ForegroundColor = colors[i];
                    Console.WriteLine(entries[i].PadRight(Console.WindowWidth - 1));
                }
            }

            Console.ResetColor();
        }
    }
}
