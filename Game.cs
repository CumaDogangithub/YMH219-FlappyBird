using System.Collections.Generic;

namespace YMH219_FlappyBird
{
    public class Game
    {
        private const int PipeSpacing = 250;
        private const int StartSpeed = 4;

        private readonly int width;

        public Bird Player { get; private set; }
        public List<Pipe> Pipes { get; private set; }
        public int Score { get; private set; }
        public int Speed { get; private set; }
        public bool IsGameOver { get; private set; }
        public int GroundY { get; private set; }

        public Game(int width, int groundY)
        {
            this.width = width;
            GroundY = groundY;
            Pipes = new List<Pipe>();
            Reset();
        }

        public void Reset()
        {
            Player = new Bird(80, GroundY / 2);
            Pipes.Clear();
            Pipes.Add(new Pipe(width, GroundY));
            Pipes.Add(new Pipe(width + PipeSpacing, GroundY));
            Score = 0;
            Speed = StartSpeed;
            IsGameOver = false;
        }

        public void Update()
        {
            if (IsGameOver) return;

            Player.Update();

            foreach (Pipe pipe in Pipes)
            {
                pipe.Move(Speed);

                if (pipe.IsOffScreen)
                    pipe.Reposition(GetRightmostX() + PipeSpacing);

                if (!pipe.Passed && pipe.X + Pipe.Width < Player.Bounds.X)
                {
                    pipe.Passed = true;
                    Score++;
                }

                if (pipe.CollidesWith(Player.Bounds))
                    IsGameOver = true;
            }

            if (Player.Bounds.Bottom >= GroundY)   // zemine çarpma
                IsGameOver = true;
        }

        private int GetRightmostX()
        {
            int max = 0;
            foreach (Pipe pipe in Pipes)
                if (pipe.X > max) max = pipe.X;
            return max;
        }
    }
}