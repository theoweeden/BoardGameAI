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
                    if (i % 2 != 1 && j % 2 == 1 ||
                        i % 2 == 1 && j % 2 != 1)
                    {
                        if (j <= 2) board[i, j] = "W";
                        else if (j >= BoardSize - 3) board[i, j] = "B";
                        else board[i, j] = " ";
                    }
                    else { 
                        board[i, j] = " "; 
                    }
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
            var opponent = NextPlayer(player);
            for (int x = 0; x < BoardSize; x++)
            {
                for (int y = 0; y < BoardSize; y++)
                {
                    if (Board[x, y].Contains(opponent)) return false;
                }
            }

            return true;
        }

        public int Evaluate(char player)
        {
            var score = 0;
            var opponent = NextPlayer(player);
            for (int x = 0; x < BoardSize; x++)
            {
                for (int y = 0; y < BoardSize; y++)
                {
                    var localScore = 0;
                    if (Board[x, y].Contains(player)) localScore += heuristicMultiplier;
                    else if (Board[x, y].Contains(opponent)) localScore -= heuristicMultiplier;

                    if (Board[x, y].Contains('K')) localScore *= 3;

                    score += localScore;
                }
            }
            return score;
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

            for (int x = 0; x < BoardSize; x++)
            {
                for (int y = 0; y < BoardSize; y++)
                {
                    if (Board[x, y].Contains(player))
                    {
                        var from = new Coords(x, y);
                        for (int i = -1; i<=1; i+=2)
                        {
                            for (int j = -1; j <= 1; j += 2)
                            {
                                var move1 = new CheckersMove(from, new Coords(x + i, y + j), player);
                                if(move1.IsValid(this)) moves.Add(move1);
                                
                                var move2 =new CheckersMove(from, new Coords(x + i * 2, y + j * 2), player);
                                if (move2.IsValid(this)) moves.Add(move2);
                            }
                        }
                    }
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

