using System;

namespace WinFormsConnectFour
{
    public enum Player { None, Red, Yellow }

    public class Board
    {
        private readonly Player[,] grid;
        public const int Rows = 6;
        public const int Cols = 7;

        public Board()
        {
            grid = new Player[Rows, Cols];
        }

        public Player GetCell(int row, int col) => grid[row, col];

        public bool DropPiece(int col, Player player, out int row)
        {
            row = -1;
            if (col < 0 || col >= Cols) return false;

            for (int r = Rows - 1; r >= 0; r--)
            {
                if (grid[r, col] == Player.None)
                {
                    grid[r, col] = player;
                    row = r;
                    return true;
                }
            }
            return false;
        }

        public bool CheckWin(Player player)
        {
            // Horizontal
            for (int r = 0; r < Rows; r++)
                for (int c = 0; c < Cols - 3; c++)
                    if (grid[r, c] == player &&
                        grid[r, c + 1] == player &&
                        grid[r, c + 2] == player &&
                        grid[r, c + 3] == player)
                        return true;

            // Vertical
            for (int c = 0; c < Cols; c++)
                for (int r = 0; r < Rows - 3; r++)
                    if (grid[r, c] == player &&
                        grid[r + 1, c] == player &&
                        grid[r + 2, c] == player &&
                        grid[r + 3, c] == player)
                        return true;

            // Diagonal (down-right)
            for (int r = 0; r < Rows - 3; r++)
                for (int c = 0; c < Cols - 3; c++)
                    if (grid[r, c] == player &&
                        grid[r + 1, c + 1] == player &&
                        grid[r + 2, c + 2] == player &&
                        grid[r + 3, c + 3] == player)
                        return true;

            // Diagonal (up-right)
            for (int r = 3; r < Rows; r++)
                for (int c = 0; c < Cols - 3; c++)
                    if (grid[r, c] == player &&
                        grid[r - 1, c + 1] == player &&
                        grid[r - 2, c + 2] == player &&
                        grid[r - 3, c + 3] == player)
                        return true;

            return false;
        }

        public bool IsFull()
        {
            for (int c = 0; c < Cols; c++)
                if (grid[0, c] == Player.None) return false;
            return true;
        }
    }
}
