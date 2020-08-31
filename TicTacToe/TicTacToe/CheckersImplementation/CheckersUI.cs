using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe
{
    class CheckersUI
    { 
        public static void Run()
        {
            IGame g = new Checkers();

            char player = Checkers.Player1;
            char opponent = g.NextPlayer(player);
            int x1, y1, x2, y2;
            while (!g.IsWon() && g.GetValidMoves(player).Any())
            {
                Console.WriteLine(g.ToString());
                do
                {
                    do
                    {
                        Console.WriteLine("Choose the X coordinate of your piece:");
                    } while (!Int32.TryParse(Console.ReadLine(), out x1));

                    do
                    {
                        Console.WriteLine("Choose the Y coordinate of your piece:");
                    } while (!Int32.TryParse(Console.ReadLine(), out y1));
                    
                    do
                    {
                        Console.WriteLine("Choose the X coordinate for your move:");
                    } while (!Int32.TryParse(Console.ReadLine(), out x2));

                    do
                    {
                        Console.WriteLine("Choose the Y coordinate for your move:");
                    } while (!Int32.TryParse(Console.ReadLine(), out y2));

                } while (!(new CheckersMove(new Coords(x1, y1), new Coords(x2, y2), player).Execute(g)));

                var move = AI.CalcNextMove(g, opponent, 5).move;
                if (move != null) move.Execute(g);
            }

            Console.WriteLine(g.ToString());
        }
    }
}
