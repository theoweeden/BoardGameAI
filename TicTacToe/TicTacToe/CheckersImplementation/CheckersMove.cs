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

        public CheckersMove(Coords from, Coords to, char player)
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

            if (Math.Abs(To.x - From.x) == 2 && Math.Abs(To.y - From.y) == 2) 
                checkers.Board[(To.x + From.x) / 2, (To.y + From.y) / 2] = " ";

            return true;
        }
        public void Undo(IGame game)
        {
            if (!(game is Checkers)) return;
            var checkers = game as Checkers;

            checkers.Board[From.x, From.y] = checkers.Board[To.x, To.y];
            checkers.Board[To.x, To.y] = " ";

            if (Math.Abs(To.x - From.x) == 2 && Math.Abs(To.y - From.y) == 2)
                checkers.Board[(To.x + From.x) / 2, (To.y + From.y) / 2] = checkers.NextPlayer(Player).ToString();//TODO Reimplement
        }
        public bool IsValid(IGame game)
        {
            if (!(game is Checkers)) return false;
            var checkers = game as Checkers;
            if (checkers.IsWon()) return false;
            if (From == null || From.x >= Checkers.BoardSize || From.y >= Checkers.BoardSize || From.x < 0|| From.y < 0) return false;
            if (To == null || To.x >= Checkers.BoardSize || To.y >= Checkers.BoardSize || To.x < 0 || To.y < 0) return false;
            if (!checkers.Board[From.x, From.y].Contains(Player)
                || checkers.Board[From.x, From.y] == " " || checkers.Board[To.x, To.y] != " ") return false;

            if (To.x == From.x || To.y == From.y) return false;

            if (Player == Checkers.Player1 && To.y <= From.y) return false;
            if (Player == Checkers.Player2 && To.y >= From.y) return false;

            if (!((Math.Abs(To.x - From.x) == 1 && Math.Abs(To.y - From.y) == 1)
                || (Math.Abs(To.x - From.x) == 2 && Math.Abs(To.y - From.y) == 2 && checkers.Board[(To.x + From.x) / 2, (To.y + From.y) / 2].Contains(checkers.NextPlayer(Player))))) return false;

            return true;
        }
    }
}
