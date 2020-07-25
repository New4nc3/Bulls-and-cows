using System;
using System.Collections.Generic;
using System.Linq;

namespace Bulls_and_cows
{
    class Program
    {
        private static NumberGenerator _numGen;
        private static List<byte> _guessedDigits;

        static void Main()
        {
            string guessedString;
            uint guessedNumber;
            uint triesCount;

            bool repeatGuessing;
            bool playAgain;

            Console.Title = "Bulls and cows";

            do
            {
                _numGen = new NumberGenerator();
                _numGen.GenerateNumberWithUniqueDigits();
                triesCount = 0;
                Console.Clear();
                Console.WriteLine($"I've randomed new {_numGen.DigitCount}-digits number. Try to guess it!");

                do
                {
                    repeatGuessing = true;
                    guessedString = Console.ReadLine();

                    if (guessedString.Length != _numGen.DigitCount || !uint.TryParse(guessedString, out guessedNumber)
                        || guessedNumber.ToString().Length != _numGen.DigitCount)
                    {
                        Console.WriteLine($"Try to input {_numGen.DigitCount}-digits positive number");
                        repeatGuessing = true;
                        continue;
                    }

                    if (IsNumberValid(guessedNumber))
                    {
                        triesCount++;
                        repeatGuessing = AnalyzeNumber(guessedNumber);
                    }

                } while (repeatGuessing);

                if (triesCount == 1)
                {
                    Console.WriteLine($"\nAbsolutely amazing! You guess my randomed number {_numGen.Randomed} at first try! Unbelievable!");
                }
                else
                {
                    Console.WriteLine($"\nYou won! I've randomed {_numGen.Randomed} and you guessed it by {triesCount} tries.");
                }

                Console.WriteLine("\nDo you want to play again?\n <Enter> - Yes\n <Any key> - No");
                playAgain = Console.ReadKey(true).Key == ConsoleKey.Enter ? true : false;

            } while (playAgain);

            Console.WriteLine("\n\nPowered by [new4nc3__#]\nPress any key to exit . . .");
            Console.ReadKey(true);
        }

        private static bool IsNumberValid(uint number)
        {
            byte digit;
            _guessedDigits = new List<byte>();

            while (number > 0)
            {
                digit = (byte)(number % 10);
                if (_guessedDigits.Contains(digit))
                {
                    Console.WriteLine("Try to input number without digit-repeating");
                    return false;
                }

                _guessedDigits.Add(digit);
                number /= 10;
            }

            _guessedDigits.Reverse();
            return true;
        }

        private static bool AnalyzeNumber(uint number)
        {
            byte bulls = 0;
            byte cows = 0;
            string output = string.Empty;
            string wordEnd;

            for (int i = 0; i < _guessedDigits.Count; i++)
            {
                if (_numGen.RandomedDigits.Contains(_guessedDigits[i]))
                {
                    if (_numGen.RandomedDigits[i] == _guessedDigits[i])
                        bulls++;
                    else
                        cows++;
                }
            }

            if (bulls == 0 && cows == 0)
            {
                output = "No bulls or cows found";
            }
            else
            {
                if (bulls > 0)
                {
                    if (bulls == _guessedDigits.Count)
                    {
                        return false;
                    }

                    wordEnd = bulls > 1 ? "s" : string.Empty;
                    output = $"Bull{wordEnd}: {bulls}. ";
                }

                if (cows > 0)
                {
                    wordEnd = cows > 1 ? "s" : string.Empty;
                    output += $"Cow{wordEnd}: {cows}.";
                }
            }

            Console.WriteLine(output);
            return true;
        }
    }
}
