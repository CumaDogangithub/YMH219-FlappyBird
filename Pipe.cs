using System;
using System.Drawing;

namespace YMH219_FlappyBird
{
    public class Pipe
    {
        public const int Width = 60;
        public const int GapHeight = 150;

        private static readonly Random random = new Random();
        private readonly int groundY;

        public int X { get; private set; }
        public int GapY { get; private set; }     // boşluğun üst kenarı
        public bool Passed { get; set; }          // skor bir kez sayılsın diye

        public Pipe(int startX, int groundY)
        {
            this.groundY = groundY;
            Reposition(startX);
        }

        public Rectangle TopBounds
        {
            get { return new Rectangle(X, 0, Width, GapY); }
        }

        public Rectangle BottomBounds
        {
            get
            {
                int top = GapY + GapHeight;
                return new Rectangle(X, top, Width, groundY - top);
            }
        }

        public bool IsOffScreen
        {
            get { return X + Width < 0; }
        }

        public void Move(int speed)
        {
            X -= speed;
        }

        public void Reposition(int newX)
        {
            X = newX;
            Passed = false;
            GapY = random.Next(50, groundY - GapHeight - 50);
        }

        public bool CollidesWith(Rectangle other)
        {
            return TopBounds.IntersectsWith(other) || BottomBounds.IntersectsWith(other);
        }
    }
}