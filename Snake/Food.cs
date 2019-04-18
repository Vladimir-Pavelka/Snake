namespace Snake.Properties
{
    using System;
    using System.Linq;

    public class Food
    {
        private readonly Random _rnd = new Random();
        public int X { get; private set; }
        public int Y { get; private set; }

        public void SpawnNew(GameArea area, SnakeBody snake)
        {
            var availableFields = area.AllFields.Except(snake.GetBody().Select(n => (n.X, n.Y))).ToArray();

            var idx = _rnd.Next(0, availableFields.Length);
            X = availableFields[idx].X;
            Y = availableFields[idx].Y;
        }
    }
}