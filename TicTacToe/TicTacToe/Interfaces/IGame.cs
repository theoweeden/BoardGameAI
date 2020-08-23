using System.Collections.Generic;

namespace TicTacToe
{
    interface IGame
    {
        public char[,] Board { get; set; }

        public bool IsWon();
        public bool IsWon(char player);
        public int Evaluate(char player);
        public char NextPlayer(char player);
        public List<IMove> GetValidMoves(char player);
        public string ToString();
    }
}
