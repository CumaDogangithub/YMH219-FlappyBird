using System.Drawing;

namespace YMH219_FlappyBird
{
    public class Bird
    {
        private const float Gravity = 0.6f;    // her karede hıza eklenen yer çekimi
        private const float JumpForce = -9f;   // zıplayınca verilen yukarı hız

        public float X { get; private set; }
        public float Y { get; private set; }
        public int Size { get; private set; }
        public float Velocity { get; private set; }

        public Bird(float x, float y, int size = 30)
        {
            X = x;
            Y = y;
            Size = size;
            Velocity = 0;
        }

        public Rectangle Bounds
        {
            get { return new Rectangle((int)X, (int)Y, Size, Size); }
        }

        public void Jump()
        {
            Velocity = JumpForce;
        }

        public void Update()
        {
            Velocity += Gravity;
            Y += Velocity;

            if (Y < 0)          // tavandan dışarı çıkmasın
            {
                Y = 0;
                Velocity = 0;
            }
        }
    }
}