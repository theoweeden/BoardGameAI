using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace TicTacToe
{
    class Checkers : IGame
    {
        public string[,] Board { get; set; }
        public const int BoardSize = 8;
        public const int heuristicMultiplier = 100;

        public Checkers()
        {
            Board = initBoard();
        }

        public string[,] initBoard()
        {
            var board = new string[BoardSize, BoardSize];

            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    board[i, j] = " ";
                }
            }

            return board;
        }

        public bool IsWon()
        {
            return IsWon('B') || IsWon('W');
        }
        public bool IsWon(char player)
        {
            return false;//TODO Implement
        }

        public int Evaluate(char player)
        {
            return 0;//TODO Implement
        }

        public char NextPlayer(char player)
        {
            return player switch
            {
                'B' => 'W',
                'W' => 'B',
                _ => ' ',
            };
        }

        public List<IMove> GetValidMoves(char player)
        {
            var moves = new List<IMove>();
            
            //TODO Implement

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

