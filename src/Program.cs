namespace Maze
{
    internal class MazeGame
    {
        private readonly static char[,] maze = {
            {'█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█'},
            {'█', ' ', ' ', ' ', '█', ' ', ' ', ' ', ' ', ' ', '█', ' ', ' ', ' ', ' ', '█', ' ', ' ', ' ', '█'},
            {'█', ' ', '█', ' ', '█', ' ', '█', '█', ' ', '█', '█', ' ', '█', '█', ' ', '█', ' ', '█', '█', '█'},
            {'█', ' ', '█', ' ', ' ', ' ', '█', ' ', ' ', ' ', ' ', ' ', '█', ' ', ' ', ' ', ' ', ' ', ' ', '█'},
            {'█', ' ', '█', '█', '█', ' ', '█', '█', '█', '█', '█', ' ', '█', '█', '█', '█', '█', '█', ' ', '█'},
            {'█', ' ', ' ', ' ', '█', ' ', ' ', ' ', ' ', ' ', '█', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '█'},
            {'█', '█', '█', ' ', '█', ' ', '█', '█', ' ', '█', '█', ' ', '█', '█', ' ', '█', '█', '█', '█', '█'},
            {'█', ' ', ' ', ' ', ' ', ' ', ' ', '█', ' ', ' ', ' ', ' ', '█', ' ', ' ', ' ', ' ', ' ', ' ', '█'},
            {'█', ' ', '█', '█', '█', '█', ' ', '█', ' ', '█', '█', ' ', '█', ' ', '█', '█', '█', ' ', '█', '█'},
            {'█', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '█'},
            {'█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█'}
        };

        private static int playerX = 1;
        private static int playerY = 1;
        private readonly static int exitX = 18;
        private readonly static int exitY = 9;

        private static void Main()
        {
            Console.CursorVisible = false;
            DrawMaze();

            while (true)
            {
                DrawPlayer(playerX, playerY);
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                MovePlayer(keyInfo);

                if (playerX == exitX && playerY == exitY)
                {
                    Console.SetCursorPosition(0, maze.GetLength(0));
                    Console.WriteLine("Поздравляю! Вы нашли выход!");
                    break;
                }
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

            if (maze[newY, newX] != ' ')
            {
                return;
            }

            MovePlayer(newX, newY);
        }

        private static void MovePlayer(int newX, int newY)
        {
            ClearPlayer(playerX, playerY);
            playerX = newX;
            playerY = newY;
            DrawPlayer(playerX, playerY);
        }

        private static void DrawPlayer(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write('@');
        }

        private static void ClearPlayer(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(' ');
        }
    }
}
