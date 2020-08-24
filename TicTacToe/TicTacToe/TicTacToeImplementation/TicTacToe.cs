using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace TicTacToe
{
    class TicTacToe : IGame
    {
        public char[,] Board { get; set; }
        public List<Coords[]> winStates { get; set; }
        public const int BoardSize = 3;
        public const int heuristicMultiplier = 100;

        public TicTacToe()
        {
            Board = initBoard();
            winStates = initWinStates();
        }

        public char[,] initBoard()
        {
            var board = new char[BoardSize, BoardSize];

            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    board[i, j] = ' ';
                }
            }

            return board;
        }

        public List<Coords[]> initWinStates()
        {
            winStates = new List<Coords[]>();

            for (int i = 0; i < BoardSize; i++)
            {
                var winStateVertical = new Coords[BoardSize];
                var winStateHorizontal = new Coords[BoardSize];
                for (int j = 0; j < BoardSize; j++)
                {
                    winStateVertical[j] = new Coords(i, j);
                    winStateHorizontal[j] = new Coords(j, i);
                }
                winStates.Add(winStateVertical);
                winStates.Add(winStateHorizontal);
            }

            var winStateDiagonal = new Coords[BoardSize];
            var winStateDiagonal2 = new Coords[BoardSize];
            for (int i = 0; i < BoardSize; i++)
            {
                winStateDiagonal[i] = new Coords(i, i);
                winStateDiagonal2[i] = new Coords(i, BoardSize - 1 - i);
            }
            winStates.Add(winStateDiagonal);
            winStates.Add(winStateDiagonal2);

            return winStates;
        }

        public bool IsWon()
        {
            return IsWon('O') || IsWon('X');
        }
        public bool IsWon(char player)
        {
            return winStates.Any(i => i.All(j => Board[j.x, j.y] == player));
        }

        public int Evaluate(char player)
        {
            return winStates.Where(i=> (i.Count(j => Board[j.x, j.y] == player) + i.Count(j => Board[j.x, j.y] == ' ')) == 3).Sum(i => (int)Math.Pow(heuristicMultiplier, i.Count(j => Board[j.x, j.y] == player)))
                - winStates.Where(i => i.Count(j => Board[j.x, j.y] == NextPlayer(player)) + i.Count(j => Board[j.x, j.y] == ' ') == 3).Sum(i => (int)Math.Pow(heuristicMultiplier, i.Count(j => Board[j.x, j.y] == NextPlayer(player))));
        }

        public char NextPlayer(char player)
        {
            return player switch
            {
                'O' => 'X',
                'X' => 'O',
                _ => ' ',
            };
        }

        public List<IMove> GetValidMoves(char player)
        {
            var moves = new List<IMove>();
            for (int x = 0; x < BoardSize; x++)
            {
                for (int y = 0; y < BoardSize; y++)
                {
                    var move = new TicTacToeMove(new Coords(x, y), player);
                    if (move.IsValid(this)) moves.Add(move);
                }
            }
            return moves;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int y = 0; y < BoardSize; y++)
            {
                sb.Append("|");
                for (int x = 0; x < BoardSize; x++)
                {
                    sb.Append(Board[x, y] + "|");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}

