using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler.Game
{
    class LevelData
    {
        private List<LevelElement> elements = new List<LevelElement>();
        public IReadOnlyList<LevelElement> Elements => elements.AsReadOnly();

        private List<Enemy> enemies = new List<Enemy>();
        public IReadOnlyList<Enemy> Enemies => enemies.AsReadOnly();


        public (int x, int y) PlayerStart { get; private set; }
        public int RatCount { get; private set; }
        public int SnakeCount { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }

        public void Load(string filename)
        {
            elements.Clear();
            enemies.Clear();

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
                            elements.Add(new Wall(x, y));
                            break;
                        case '@':
                            PlayerStart = (x, y);
                            break;
                        case 'r':
                            var rat = new Rat(x, y);
                            enemies.Add(rat);
                            elements.Add(rat);
                            break;
                        case 's':
                            var snake = new Snake(x, y);
                            enemies.Add(snake);
                            elements.Add(snake);
                            break;
                        default:
                            break;
                    }
                }
            }
            Height = lines.Length;
            Width = 0;
            foreach (var line in lines)
            {
                if (line.Length > Width)
                    Width = line.Length;
            }
        }
    }
}
