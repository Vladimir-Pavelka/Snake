namespace Snake
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GameArea
    {
        private readonly int _width;
        private readonly int _height;

        public Point TopLeft { get; }
        public Point TopRight { get; }
        public Point BotLeft { get; }
        public Point BotRight { get; }

        public int Score { get; set; }

        public GameArea(int width, int height, int leftOffset = 0, int topOffset = 0)
        {
            TopLeft = new Point(leftOffset, topOffset);
            TopRight = new Point(leftOffset + width, topOffset);
            BotLeft = new Point(leftOffset, topOffset + height);
            BotRight = new Point(leftOffset + width, topOffset + height);
            _width = width;
            _height = height;

            AllFields = Enumerable.Range(leftOffset + 1, width - 1).SelectMany(x =>
                Enumerable.Range(topOffset + 1, height - 1).Select(y => (x, y))).ToHashSet();
        }

        public HashSet<(int X, int Y)> AllFields { get; }

        public void Print()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            WriteXy(TopLeft.X, TopLeft.Y, '┌');
            for (int i = 0; i < _width - 1; i++) Console.Write('─');
            Console.Write('┐');
            for (int i = 1; i < _height; i++) WriteXy(TopLeft.X, TopLeft.Y + i, '│');
            for (int i = 1; i < _height; i++) WriteXy(TopRight.X, TopRight.Y + i, '│');
            WriteXy(BotLeft.X, BotLeft.Y, '└');
            for (int i = 0; i < _width - 1; i++) Console.Write('─');
            Console.Write('┘');
        }

        private void WriteXy(int x, int y, char c)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(c);
        }

        public void PrintScore()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(66, 3);
            Console.Write($"Score: {Score}");
        }

        public void PrintGameOver()
        {
            Console.SetCursorPosition(26, 11);
            Console.Write("            ");
            Console.SetCursorPosition(26, 12);
            Console.Write(" Game Over! ");
            Console.SetCursorPosition(26, 13);
            Console.Write("            ");
        }
    }
}