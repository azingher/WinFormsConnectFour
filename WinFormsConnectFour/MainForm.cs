using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsConnectFour
{
    public class MainForm : Form
    {
        private readonly Game game;
        private const int CellSize = 80;

        public MainForm()
        {
            this.Text = "Connect Four in Winforms";
            this.ClientSize = new Size(Board.Cols * CellSize, Board.Rows * CellSize + 40);
            this.DoubleBuffered = true;
            this.game = new Game();

            this.MouseClick += OnMouseClick;
            this.Paint += OnPaint;
        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            int col = e.X / CellSize;
            if (game.MakeMove(col, out int row, out Player winner))
            {
                Invalidate();

                if (winner != Player.None)
                {
                    MessageBox.Show($"{winner} wins!");
                    Application.Restart(); // quick reset
                }
                else if (game.Board.IsFull())
                {
                    MessageBox.Show("It's a draw!");
                    Application.Restart();
                }
            }
        }



        private void OnPaint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            var thickPen = new Pen(Color.Black, 2);

            for (int r = 0; r < Board.Rows; r++)
            {
                for (int c = 0; c < Board.Cols; c++)
                {
                    Rectangle rect = new Rectangle(c * CellSize, r * CellSize, CellSize, CellSize);
                    Rectangle ellip = new Rectangle(c * CellSize + 2, r * CellSize + 2, CellSize - 4, CellSize- 4);
                    g.FillRectangle(Brushes.Black, rect);
                    g.DrawRectangle(thickPen, rect);

                    var piece = game.Board.GetCell(r, c);
                    if (piece != Player.None)
                    {
                        Brush brush = (piece == Player.Red) ? Brushes.Red : Brushes.Yellow;
                        g.FillEllipse(brush, ellip);
                    }
                    else
                    {
                        g.FillEllipse(Brushes.White, ellip);
                    }
                }
            }
        }
    }
}
