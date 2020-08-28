using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    interface IMove
    {
        public bool Execute(IGame game);
        public void Undo(IGame game);
        public bool IsValid(IGame game);
        public int Evaluate(IGame game, char player);
    }
}
