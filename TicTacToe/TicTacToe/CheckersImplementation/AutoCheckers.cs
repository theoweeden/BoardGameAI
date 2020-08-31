using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe
{
    class AutoCheckers
    { 
        public static void Run()
        {
            IGame g = new Checkers();

            char player = Checkers.Player1;
            char opponent = g.NextPlayer(player);
            while (!g.IsWon() && g.GetValidMoves(player).Any())
            {
                Console.WriteLine(g.ToString());

                var move1 = AI.CalcNextMove(g, player, 5).move;
                if (move1 != null) move1.Execute(g);

                Console.WriteLine(g.ToString());

                var move2 = AI.CalcNextMove(g, opponent, 5).move;
                if (move2 != null) move2.Execute(g);
            }

            Console.WriteLine(g.ToString());
        }
    }
}
