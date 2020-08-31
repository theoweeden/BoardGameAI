using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace TicTacToe
{
    class Checkers : IGame
    {
        public CheckersPiece[,] Board { get; set; }
        public const int BoardSize = 8;
        public const int heuristicMultiplier = 100;

        public const char Player1 = 'W';
        public const char Player2 = 'B';

        public Checkers()
        {
            Board = initBoard();
        }

        public CheckersPiece[,] initBoard()
        {
            var board = new CheckersPiece[BoardSize, BoardSize];

            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (i % 2 != 1 && j % 2 == 1 ||
                        i % 2 == 1 && j % 2 != 1)
                    {
                        if (j <= 2) board[i, j] = new CheckersPiece(Player1, false);
                        else if (j >= BoardSize - 3) board[i, j] = new CheckersPiece(Player2, false);
                        else board[i, j] = null;
                    }
                    else { 
                        board[i, j] = null; 
                    }
                }
            }

            return board;
        }

        public bool IsWon()
        {
            return IsWon(Player1) || IsWon(Player2);
        }
        public bool IsWon(char player)
        {
            var opponent = NextPlayer(player);
            for (int x = 0; x < BoardSize; x++)
            {
                for (int y = 0; y < BoardSize; y++)
                {
                    if (Board[x, y] != null && Board[x, y].Player == opponent) return false;
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
                    if (Board[x, y] != null)
                    {
                        var localScore = 0;
                        if (Board[x, y].Player == player) localScore += heuristicMultiplier;
                        else if (Board[x, y].Player == opponent) localScore -= heuristicMultiplier;

                        if (Board[x, y].King) localScore *= 3;

                        score += localScore;
                    }
                }
            }
            return score;
        }

        public char NextPlayer(char player)
        {
            return player switch
            {
                Player1 => Player2,
                Player2 => Player1,
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
                    if (Board[x, y] != null && Board[x, y].Player == player)
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
                        var c = Board[x, y]?.Player ?? ' ';

                        if (!Board[x, y]?.King ?? false) c = char.ToLower(c);

                    sb.Append(c + "|");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}

