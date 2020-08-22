using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    interface IMove
    {
        public void Execute(IGame game);
        public void Undo(IGame game);
        public bool IsValid(IGame game);
    }
}
