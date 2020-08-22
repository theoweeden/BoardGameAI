using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    class AI
    {
        public static (Coords move, int score) CalcNextMove(IGame game, char player, char original, int ply)
        {
            if (ply == 0 || !game.ValidMovesExist())
            {
                return (null, game.Evaluate(original));
            }

            (Coords position, int score) best = (null, 0);
            //try out moves
            for (int x = 0; x < IGame.BoardSize; x++)
            {
                for (int y = 0; y < IGame.BoardSize; y++)
                {
                    if (game.Board[x, y] == ' ')
                    {
                        game.Board[x, y] = player;
                        var (_, score) = CalcNextMove(game, game.getOtherPlayer(player), original, ply - 1);
                        game.Board[x, y] = ' ';

                        if (best.position == null) best = (new Coords(x, y), score);

                        if (player == original)
                        {
                            if (score > best.score) best = (new Coords(x, y), score);
                        }
                        else
                        {
                            if (score < best.score) best = (new Coords(x, y), score);
                        }
                    }
                }
            }

            return best;
        }
    }
}
