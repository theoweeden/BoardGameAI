using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe
{
    class AI
    {
        public static (IMove move, int score) CalcNextMove(IGame game, char player, char original, int ply)
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
                    var (_, score) = CalcNextMove(game, game.getOtherPlayer(player), original, ply - 1);
                    move.Undo(game);

                    if (best.move == null) best = (move, score);

                    if (player == original)
                    {
                        if (score > best.score) best = (move, score);
                    }
                    else
                    {
                        if (score < best.score) best = (move, score);
                    }
                }
            }

            return best;
        }
    }
}
