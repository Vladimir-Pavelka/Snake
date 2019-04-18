namespace Snake
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using Properties;

    class Program
    {
        private static Renderer _renderer = new Renderer();
        private static PlayerInput _input = new PlayerInput();
        private static GameArea _area = new GameArea(60, 22, 1, 1);
        private static Stopwatch _stopwatch = new Stopwatch();
        private static SnakeBody _snake;
        private static Food _food = new Food();

        private const int wantedFrameLengthMs = 50;

        static void Main()
        {
            Console.CursorVisible = false;

            var startNewGame = true;
            while (startNewGame)
            {
                StartNewGame();
                _area.PrintGameOver();
                startNewGame = _input.ConsumeCharsUntilEnterOrEscape();
            }
        }

        private static void StartNewGame()
        {
            Initialize();

            var isGameOver = false;
            while (!isGameOver)
            {
                isGameOver = Update();
                RegulateGameSpeed();
            }
        }

        private static void Initialize()
        {
            Console.Clear();
            _area.Print();
            _area.Score = 0;
            _area.PrintScore();
            _snake = new SnakeBody(15, 12, 10);
            _input.Clear();
            _food.SpawnNew(_area, _snake);
            _stopwatch.Restart();
        }

        private static bool Update()
        {
            _snake.Direction = _input.GetNewDirection();
            var oldTail = _snake.Tail;
            _snake.Move();

            if (_snake.GetBody().Skip(1).Any(n => Equals(n, _snake.Head))) return true;
            if (!_area.AllFields.Contains((_snake.Head.X, _snake.Head.Y))) return true;

            if (IsPositionEqual(_snake.Head, _food))
            {
                _area.Score++;
                _area.PrintScore();
                _snake.Grow(oldTail);
                _food.SpawnNew(_area, _snake);
                _renderer.Redraw(_snake.Head);
            }
            else
            {
                _renderer.Redraw(_snake.Head, oldTail);
            }

            _renderer.Draw(_food);
            return false;
        }

        static bool IsPositionEqual(SnakeBody.Node node, Food food) =>
            node.X == food.X && node.Y == food.Y;

        static bool Equals(SnakeBody.Node a, SnakeBody.Node b) => a.X == b.X && a.Y == b.Y;

        private static void RegulateGameSpeed()
        {
            Thread.Sleep(wantedFrameLengthMs - (int)_stopwatch.ElapsedMilliseconds);
            _stopwatch.Restart();
        }
    }
}
