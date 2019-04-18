namespace Snake
{
    using System;
    using Properties;

    public class Renderer
    {
        public void Redraw(SnakeBody.Node newHead)
        {
            RedrawHead(newHead);
        }

        public void Redraw(SnakeBody.Node newHead, SnakeBody.Node oldTail)
        {
            RedrawHead(newHead);
            RedrawTail(oldTail);
        }

        private void RedrawHead(SnakeBody.Node newHead)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(newHead.X, newHead.Y);
            Console.Write('#');
        }

        private void RedrawTail(SnakeBody.Node oldTail)
        {
            Console.SetCursorPosition(oldTail.X, oldTail.Y);
            Console.Write(' ');
        }

        public void Draw(Food food)
        {
            Console.SetCursorPosition(food.X, food.Y);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write('o');
        }
    }
}