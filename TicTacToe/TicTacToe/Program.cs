using System;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            IGame t = new TicTacToe();

            char player = 'X';
            char opponent = t.getOtherPlayer(player);
            int x, y;
            while (!t.isWon('X') && !t.isWon('O') && t.ValidMovesExist())
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

                } while (!t.DoMoveIfValid(new TicTacToeMove(new Coords(x, y), player)));

                t.DoMoveIfValid(new TicTacToeMove(AI.CalcNextMove(t, opponent, opponent, 9).move, opponent));
            }

            Console.WriteLine(t.ToString());
        }
    }
}
