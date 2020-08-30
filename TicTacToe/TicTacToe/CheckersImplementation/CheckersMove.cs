using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    class CheckersMove : IMove
    {
        public Coords From { get; set; }
        public Coords To { get; set; }
        public char Player { get; set; }

        public CheckersMove(Coords from,Coords to,char player)
        {
            From = from;
            To = to;
            Player = player;
        }

        public bool Execute(IGame game)
        {
            if (!(game is Checkers) || !IsValid(game)) return false;

            var checkers = game as Checkers;

            checkers.Board[To.x, To.y] = checkers.Board[From.x, From.y];
            checkers.Board[From.x, From.y] = " ";
            return true;
        }
        public void Undo(IGame game)
        {
            if (!(game is Checkers)) return;
            var checkers = game as Checkers;

            checkers.Board[From.x, From.y] = checkers.Board[To.x, To.y];
            checkers.Board[To.x, To.y] = " ";
        }
        public bool IsValid(IGame game)
        {
            if (!(game is Checkers)) return false;
            var checkers = game as Checkers;
            if ((checkers.IsWon()
                || From == null || From.x >= TicTacToe.BoardSize || From.y >= TicTacToe.BoardSize
                || To == null || To.x >= TicTacToe.BoardSize || To.y >= TicTacToe.BoardSize
                || !checkers.Board[From.x, From.y].Contains(Player)
                || checkers.Board[From.x, From.y] == " " || checkers.Board[To.x, To.y] != " ")) return false;
         
            
            return true;
        }
    }
}
