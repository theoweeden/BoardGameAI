using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe
{
    class TicTacToeUI
    { 
        public static void Run()
        {
            IGame t = new TicTacToe();

            char player = TicTacToe.Player1;
            char opponent = t.NextPlayer(player);
            int x, y;
            while (!t.IsWon() && t.GetValidMoves(player).Any())
            {
                Console.WriteLine(t.ToString());
                do
                {
                    do
                    {
                        Console.WriteLine("Choose the X coordinate for your move:");
                    } while (!Int32.TryParse(Console.ReadLine(), out x));

                    do
                    {
                        Console.WriteLine("Choose the Y coordinate for your move:");
                    } while (!Int32.TryParse(Console.ReadLine(), out y));

                } while (!(new TicTacToeMove(new Coords(x, y), player).Execute(t)));

                var move = AI.CalcNextMove(t, opponent).move;
                if (move != null) move.Execute(t);
            }

            Console.WriteLine(t.ToString());
        }
    }
}
