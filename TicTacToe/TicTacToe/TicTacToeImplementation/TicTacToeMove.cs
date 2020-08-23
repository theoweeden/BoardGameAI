using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    class TicTacToeMove : IMove
    {
        public Coords Coords { get; set; }
        public char Player { get; set; }

        public TicTacToeMove(Coords coords,char player)
        {
            Coords = coords;
            Player = player;
        }

        public bool Execute(IGame game)
        {
            if (!IsValid(game)) return false;
            
            game.Board[Coords.x, Coords.y] = Player;
            return true;
        }
        public void Undo(IGame game)
        {
            game.Board[Coords.x, Coords.y] = ' ';
        }
        public bool IsValid(IGame game)
        {
            return !(game.isWon('O') || game.isWon('X') || Coords == null || Coords.x > 2 || Coords.y > 2 || game.Board[Coords.x, Coords.y] != ' ');
        }
    }
}
