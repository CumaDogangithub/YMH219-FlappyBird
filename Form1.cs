using System;
using System.Drawing;
using System.Windows.Forms;

namespace YMH219_FlappyBird
{
    public partial class Form1 : Form
    {
        private const int GroundHeight = 60;

        private readonly Font scoreFont = new Font("Segoe UI", 16, FontStyle.Bold);
        private readonly Font titleFont = new Font("Segoe UI", 28, FontStyle.Bold);
        private Game game;

        public Form1()
        {
            InitializeComponent();

            ClientSize = new Size(400, 500);
            DoubleBuffered = true;   // titremeyi önler
            KeyPreview = true;       // tuşlar önce forma gelsin

            btnRestart.Location = new Point((ClientSize.Width - btnRestart.Width) / 2, 240);
            btnRestart.Visible = false;

            game = new Game(ClientSize.Width, ClientSize.Height - GroundHeight);
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            game.Update();

            if (game.IsGameOver)
            {
                gameTimer.Stop();
                btnRestart.Visible = true;
            }

            Invalidate();   // formu yeniden çizdirir, Paint olayını tetikler
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                e.SuppressKeyPress = true;
                if (game.IsGameOver) return;

                if (!gameTimer.Enabled) gameTimer.Start();   // ilk Space oyunu başlatır
                game.Player.Jump();
            }
            else if (e.KeyCode == Keys.R && game.IsGameOver)
            {
                RestartGame();
            }
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            RestartGame();
        }

        private void RestartGame()
        {
            game.Reset();
            btnRestart.Visible = false;
            Focus();
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.SkyBlue);

            foreach (Pipe pipe in game.Pipes)
            {
                g.FillRectangle(Brushes.ForestGreen, pipe.TopBounds);
                g.FillRectangle(Brushes.ForestGreen, pipe.BottomBounds);
            }

            g.FillRectangle(Brushes.SandyBrown, 0, game.GroundY, ClientSize.Width, GroundHeight);
            g.FillEllipse(Brushes.Gold, game.Player.Bounds);

            g.DrawString("Skor: " + game.Score, scoreFont, Brushes.White, 10, 10);

            if (game.IsGameOver)
                DrawCentered(g, "GAME OVER", titleFont, Brushes.Red, 150);
            else if (!gameTimer.Enabled)
                DrawCentered(g, "Başlamak için SPACE", scoreFont, Brushes.White, 180);
        }

        private void DrawCentered(Graphics g, string text, Font font, Brush brush, int y)
        {
            SizeF size = g.MeasureString(text, font);
            g.DrawString(text, font, brush, (ClientSize.Width - size.Width) / 2, y);
        }
    }
}