using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    class CheckersPiece
    {
        public char Player { get; }
        public bool King { get; set; }

        public CheckersPiece(char player, bool king)
        {
            Player = player;
            King = king;
        }
    }
}
