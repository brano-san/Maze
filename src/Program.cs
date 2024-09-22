namespace Maze
{
    internal class MazeGame
    {
        // Ширина лабиринта (нечётное число)
        private readonly static int width = 43;
        // Высота лабиринта (нечётное число)
        private readonly static int height = 21;

        // Константы графики
        private readonly static char wallSymbol = '█';
        private readonly static char playerSymbol = 'P';
        private readonly static char spaceSymbol = ' ';

        // Лабиринт
        private static char[,] maze;

        // Игрок
        private static int playerX = 1;
        private static int playerY = 1;

        // Выход
        private static int exitX;
        private static int exitY;

        private static void Main(string[] args)
        {
            Console.CursorVisible = false;

            GenerateMaze();
            DrawMaze();

            while (true)
            {
                DrawPlayer(playerX, playerY);
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                MovePlayer(keyInfo);

                if (playerX == exitX && playerY == exitY)
                {
                    Console.SetCursorPosition(0, height);
                    Console.WriteLine("Поздравляю! Вы нашли выход!");
                    break;
                }
            }
        }

        private static void GenerateMaze()
        {
            maze = new char[height, width];
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    maze[y, x] = wallSymbol;
                }
            }

            CarvePassagesFrom(1, 1);

            exitX = width - 2;
            exitY = height - 2;
            maze[exitY, exitX] = spaceSymbol;
        }

        private static void CarvePassagesFrom(int currentX, int currentY)
        {
            var directions = new int[][] {
                new int[] { 2, 0 },
                new int[] { -2, 0 },
                new int[] { 0, 2 },
                new int[] { 0, -2 }
            };

            Shuffle(directions);

            foreach (var dir in directions)
            {
                var newX = currentX + dir[0];
                var newY = currentY + dir[1];

                if (newX > 0 && newX < width - 1 && newY > 0 && newY < height - 1 && maze[newY, newX] == wallSymbol)
                {
                    maze[newY, newX] = ' ';
                    maze[currentY + dir[1] / 2, currentX + dir[0] / 2] = ' ';
                    CarvePassagesFrom(newX, newY);
                }
            }
        }

        private static void Shuffle(int[][] array)
        {
            for (var i = array.Length - 1; i > 0; i--)
            {
                var randIndex = new Random().Next(i + 1);
                (array[i], array[randIndex]) = (array[randIndex], array[i]);
            }
        }

        private static void DrawMaze()
        {
            for (var y = 0; y < maze.GetLength(0); y++)
            {
                for (var x = 0; x < maze.GetLength(1); x++)
                {
                    Console.Write(maze[y, x]);
                }
                Console.WriteLine();
            }
        }

        private static void DrawPlayer(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(playerSymbol);
        }

        private static void ClearPlayer(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(spaceSymbol);
        }

        private static void MovePlayer(ConsoleKeyInfo keyInfo)
        {
            var newX = playerX;
            var newY = playerY;

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    newY--;
                    break;
                case ConsoleKey.DownArrow:
                    newY++;
                    break;
                case ConsoleKey.LeftArrow:
                    newX--;
                    break;
                case ConsoleKey.RightArrow:
                    newX++;
                    break;
            }

            // Игрок может двигаться только по пустому пространству
            if (maze[newY, newX] != spaceSymbol)
            {
                return;
            }

            ClearPlayer(playerX, playerY);
            playerX = newX;
            playerY = newY;
            DrawPlayer(playerX, playerY);
        }
    }
}
