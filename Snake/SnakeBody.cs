namespace Snake
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class SnakeBody
    {
        public SnakeBody(int startX, int startY, int length)
        {
            if (length < 1) throw new ArgumentException(nameof(length));

            Direction = Direction.East;
            Head = new Node(startX, startY);
            var nodes = Enumerable.Range(1, length).Select(offset => new Node(startX - offset, startY)).ToList();
            var previous = Head;
            nodes.ForEach(n =>
            {
                previous.Next = n;
                n.Previous = previous;
                previous = n;
            });
            Tail = nodes.Last();
        }

        public Direction Direction { get; set; }
        public Node Head { get; private set; }
        public Node Tail { get; private set; }

        public IEnumerable<Node> GetBody()
        {
            var node = Head;
            while (node != null)
            {
                yield return node;
                node = node.Next;
            }
        }

        public void Move()
        {
            Tail.Previous.Next = null;
            Tail = Tail.Previous;
            var newHead = new Node(GetNewX(), GetNewY());
            newHead.Next = Head;
            Head.Previous = newHead;
            Head = newHead;
        }

        public void Grow(Node increment)
        {
            Tail.Next = increment;
            increment.Previous = Tail;
            Tail = increment;
        }

        private int GetNewX() =>
            Head.X + (Direction == Direction.North || Direction == Direction.South ? 0 : Direction == Direction.West ? -1 : 1);

        private int GetNewY() =>
            Head.Y + (Direction == Direction.West || Direction == Direction.East ? 0 : Direction == Direction.North ? -1 : 1);

        [DebuggerDisplay("({X},{Y})")]
        public class Node
        {
            public Node(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; }
            public int Y { get; }
            public Node Previous { get; set; }
            public Node Next { get; set; }
        }
    }

    public enum Direction
    {
        North,
        East,
        South,
        West
    }
}