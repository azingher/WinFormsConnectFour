
namespace WinFormsConnectFour
{
    public class Game
    {
        public Board Board { get; }
        public Player CurrentPlayer { get; private set; }

        public Game()
        {
            Board = new Board();
            CurrentPlayer = Player.Red;
        }

        public bool MakeMove(int col, out int row, out Player winner)
        {
            winner = Player.None;
            if (!Board.DropPiece(col, CurrentPlayer, out row))
                return false;

            if (Board.CheckWin(CurrentPlayer))
                winner = CurrentPlayer;
            else
                SwitchPlayer();

            return true;
        }

        private void SwitchPlayer()
        {
            CurrentPlayer = (CurrentPlayer == Player.Red) ? Player.Yellow : Player.Red;
        }
    }
}
