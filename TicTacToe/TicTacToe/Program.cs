using System;
using System.Linq;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            IGame t = new TicTacToe();

            char player = 'X';
            char opponent = t.NextPlayer(player);
            int x, y;
            while (!t.IsWon('X') && !t.IsWon('O') && !t.IsWon() && t.GetValidMoves(player).Any())
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

                var move = AI.CalcNextMove(t, opponent, opponent, 9).move;
                if(move != null)move.Execute(t);
            }

            Console.WriteLine(t.ToString());
        }
    }
}
