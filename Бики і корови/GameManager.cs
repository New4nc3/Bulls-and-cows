using System;
using System.Collections.Generic;
using System.Linq;

namespace Bulls_and_cows
{
    sealed class GameManager
    {
        public uint TriesCount { get; private set; } = 0;

        private NumberGenerator _numGen;
        private List<byte> _guessedDigits;

        private string _guessedString;
        private uint _guessedNumber;

        private bool _repeatGuessing;
        private bool _playAgain;

        public void StartGame()
        {
            do
            {
                _numGen = new NumberGenerator();
                _numGen.GenerateNumberWithUniqueDigits();
                TriesCount = 0;

                Console.Clear();
                Console.WriteLine($"I've randomed new {_numGen.DigitCount}-digits number. Try to guess it!");

                do
                {
                    _repeatGuessing = true;
                    _guessedString = Console.ReadLine();

                    if (_guessedString.Length != _numGen.DigitCount || !uint.TryParse(_guessedString, out _guessedNumber)
                        || _guessedNumber.ToString().Length != _numGen.DigitCount || !IsNumberValid(_guessedNumber))
                    {
                        Console.WriteLine($"Try to input {_numGen.DigitCount}-digits positive number");
                        _repeatGuessing = true;
                        continue;
                    }

                    TriesCount++;
                    _repeatGuessing = AnalyzeGuessedNumber();

                } while (_repeatGuessing);

                if (TriesCount == 1)
                {
                    Console.WriteLine($"\nAbsolutely amazing! You guess my randomed number {_numGen.Randomed} at first try! Unbelievable!");
                }
                else
                {
                    Console.WriteLine($"\nYou won! I've randomed {_numGen.Randomed} and you guessed it by {TriesCount} tries.");
                }

                Console.WriteLine("\nDo you want to play again?\n <Enter> - Yes\n <Any key> - No");
                _playAgain = Console.ReadKey(true).Key == ConsoleKey.Enter ? true : false;

            } while (_playAgain);
        }

        private bool IsNumberValid(uint number)
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

        private bool AnalyzeGuessedNumber()
        {
            byte bulls = 0;
            byte cows = 0;
            string output = string.Empty;

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

                    output = $"Bull{GetCorrectEndChar(bulls)}: {bulls}. ";
                }

                if (cows > 0)
                {
                    output += $"Cow{GetCorrectEndChar(cows)}: {cows}.";
                }
            }

            Console.WriteLine(output);
            return true;
        }

        private string GetCorrectEndChar(int entity)
        {
            return entity > 1 ? "s" : string.Empty;
        }
    }
}
