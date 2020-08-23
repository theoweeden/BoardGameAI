namespace TicTacToe
{
    interface IGame
    {
        public char[,] Board { get; set; }
        public const int BoardSize = 3;

        public bool isWon(char player);
        public int Evaluate(char player);
        public char getOtherPlayer(char player);
        public bool DoMoveIfValid(IMove move);
        public bool ValidMovesExist();
        public string ToString();
    }
}
