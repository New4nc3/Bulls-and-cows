﻿using System;

namespace Bulls_and_cows
{
    class Program
    {
        static void Main()
        {
            Console.Title = "Bulls and cows";

            GameManager bullsAndCowsGame = new GameManager();
            bullsAndCowsGame.StartGame();

            Console.WriteLine("\n\nPowered by [new4nc3__#]\nPress any key to exit . . .");
            Console.ReadKey(true);
        }
    }
}
