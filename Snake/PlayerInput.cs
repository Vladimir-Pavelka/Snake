namespace Snake
{
    using System;

    public class PlayerInput
    {
        private Direction _lastDirection = Direction.East;

        public Direction GetNewDirection()
        {
            if (!Console.KeyAvailable) return _lastDirection;
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    if (_lastDirection != Direction.South) _lastDirection = Direction.North;
                    break;

                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    if (_lastDirection != Direction.North) _lastDirection = Direction.South;
                    break;

                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    if (_lastDirection != Direction.East) _lastDirection = Direction.West;
                    break;

                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    if (_lastDirection != Direction.West) _lastDirection = Direction.East;
                    break;
            }

            return _lastDirection;
        }

        public bool ConsumeCharsUntilEnterOrEscape()
        {
            var keyPress = ConsoleKey.NoName;
            while (keyPress != ConsoleKey.Enter && keyPress != ConsoleKey.Escape)
                keyPress = Console.ReadKey(true).Key;

            var isRestartGame = keyPress == ConsoleKey.Enter;
            return isRestartGame;
        }

        public void Clear() => _lastDirection = Direction.East;
    }
}