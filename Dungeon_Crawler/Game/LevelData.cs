using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dungeon_Crawler.Game
{
    class LevelData
    {
        public List<LevelElement> Elements { get; private set; } = new List<LevelElement>();
        public List<Enemy> Enemies { get; private set; } = new List<Enemy>();

        public (int x, int y) PlayerStart { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }


        public void Load(string filename)
        {
            Elements.Clear();
            Enemies.Clear();

            var lines = File.ReadAllLines(filename);

            for (int y = 0; y < lines.Length; y++)
            {
                string line = lines[y];

                for (int x = 0; x < line.Length; x++)
                {
                    char ch = line[x];
                    switch (ch)
                    {
                        case '#':
                            Elements.Add(new Wall(x, y));
                            break;

                        case '@':
                            PlayerStart = (x, y);
                            break;

                        case 'r':
                            var rat = new Rat(x, y);
                            Enemies.Add(rat);
                            Elements.Add(rat);
                            break;

                        case 's':
                            var snake = new Snake(x, y);
                            Enemies.Add(snake);
                            Elements.Add(snake);
                            break;
                    }
                }
            }

            Height = lines.Length;
            Width = lines.Max(line => line.Length);
        }
    }
}
