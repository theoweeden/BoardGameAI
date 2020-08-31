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
            if (!(game is TicTacToe) || !IsValid(game)) return false;

            var tictactoe = game as TicTacToe;
            tictactoe.Board[Coords.x, Coords.y] = Player;
            return true;
        }
        public void Undo(IGame game)
        {
            if (!(game is TicTacToe)) return;

            var tictactoe = game as TicTacToe;
            tictactoe.Board[Coords.x, Coords.y] = ' ';
        }
        public bool IsValid(IGame game)
        {
            if (!(game is TicTacToe)) return false;
            var tictactoe = game as TicTacToe;
            return !(tictactoe.IsWon() || Coords == null || Coords.x >= TicTacToe.BoardSize || Coords.y >= TicTacToe.BoardSize || tictactoe.Board[Coords.x, Coords.y] != ' ');
        }
        public int Evaluate(IGame game, char player)
        {
            Execute(game);
            var score = game.Evaluate(player);
            Undo(game);
            return score;
        }
    }
}
