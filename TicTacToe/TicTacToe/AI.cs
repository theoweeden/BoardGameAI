using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe
{
    class AI
    {
        public static (IMove move, int score) CalcNextMove(IGame game, char player, int ply)
        {
            return AlphaBeta(game, player, player, ply, int.MinValue, int.MaxValue);
        }

        public static (IMove move, int score) AlphaBeta(IGame game, char player, char original, int ply, int alpha, int beta)
        {
            if (ply == 0 || game.IsWon() || !game.GetValidMoves(player).Any())
            {
                return (null, game.Evaluate(original));
            }

            (IMove move, int score) best = (null, 0);

            var moves = game.GetValidMoves(player);

            foreach (var move in moves) { 
                if (move.IsValid(game))
                {
                    move.Execute(game);
                    var (_, score) = AlphaBeta(game, game.NextPlayer(player), original, ply - 1, alpha, beta);
                    move.Undo(game);

                    if (best.move == null) best = (move, score);

                    if (player == original)
                    {
                        if (score > best.score) best = (move, score);

                        if (alpha < best.score) alpha = best.score;

                        if (alpha >= beta) break;
                    }
                    else
                    {
                        if (score < best.score) best = (move, score);

                        if (beta > best.score) beta = best.score;

                        if (beta <= alpha) break;
                    }
                }
            }

            return best;
        }
    }
}
