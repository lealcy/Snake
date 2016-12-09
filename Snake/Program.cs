using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace Snake
{
    class Program
    {
        enum Direction
        {
            UP, RIGHT, DOWN, LEFT
        }

        static void Main(string[] args)
        {
            var random = new Random();
            var head = new Point(Console.WindowWidth / 2, Console.WindowHeight / 2);
            var tail = new List<Point>();
            var target = new Point(random.Next(0, Console.WindowWidth), random.Next(0, Console.WindowHeight));
            int score = 0;
            Direction dir = Direction.RIGHT;
            bool lose = false;

            Console.Title = "Snake";

            while (true)
            {
                Console.SetCursorPosition(head.X, head.Y);
                Console.Write(" ");
                if (tail.Count > 0)
                {
                    Point last = tail.Last();
                    Console.SetCursorPosition(last.X, last.Y);
                    Console.Write(" ");
                }

                if (Console.KeyAvailable)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.UpArrow: dir = Direction.UP; break;
                        case ConsoleKey.RightArrow: dir = Direction.RIGHT; break;
                        case ConsoleKey.DownArrow: dir = Direction.DOWN; break;
                        case ConsoleKey.LeftArrow: dir = Direction.LEFT; break;
                        case ConsoleKey.Escape: return;
                    }
                }
                if (tail.Count > 0)
                {
                    if (tail.Count > 1)
                    {
                        for (int i = tail.Count - 1; i > 0; i--)
                        {
                            tail[i].Set(tail[i - 1]);
                        }
                    }
                    tail[0].Set(head);
                }

                switch (dir)
                {
                    case Direction.UP: head.Add(0, -1); break;
                    case Direction.RIGHT: head.Add(1, 0); break;
                    case Direction.DOWN: head.Add(0, 1); break;
                    case Direction.LEFT: head.Add(-1, 0); break;
                }
                if (head.X < 0 || head.Y < 0 || head.X > Console.WindowWidth - 1 || head.Y > Console.WindowHeight - 2)
                {
                    lose = true;
                    break;
                }
                foreach (var segment in tail)
                {
                    if (segment == head)
                    {
                        lose = true;
                        break;
                    }
                }
                if (lose)
                {
                    break;
                }
                if (head == target)
                {
                    tail.Add(head.Copy());
                    target.Set(random.Next(0, Console.WindowWidth), random.Next(0, Console.WindowHeight));
                    score += 10;
                }
                Console.SetCursorPosition(target.X, target.Y);
                Console.Write("$");
                foreach (var segment in tail)
                {
                    Console.SetCursorPosition(segment.X, segment.Y);
                    Console.Write("*");

                }
                Console.SetCursorPosition(head.X, head.Y);
                Console.Write("@");

                Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight - 1);
                Console.Write("Score: {0}", score);

                Thread.Sleep(100);
            }
            if (lose)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2 - 1);
                Console.Write("You Lose!");
                Console.ReadKey(true);
            }
        }
    }

    class Point
    {
        public int X;
        public int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Set(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Set(Point p)
        {
            X = p.X;
            Y = p.Y;
        }

        public Point Copy()
        {
            return new Point(X, Y);
        }

        public void Add(int x, int y)
        {
            X += x;
            Y += y;
        }

        public static bool operator == (Point r, Point l)
        {
            return r.X == l.X && r.Y == l.Y;
        }

        public static bool operator !=(Point r, Point l)
        {
            return r.X != l.X || r.Y != l.Y;
        }
    }
}
