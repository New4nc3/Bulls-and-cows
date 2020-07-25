using System;
using System.Collections.Generic;

namespace Bulls_and_cows
{
    class NumberGenerator
    {
        public int DigitCount { get; } = 4;
        public byte[] RandomedDigits { get; } = new byte[4];
        public uint Randomed
        {
            get
            {
                if (randomed_ == 0)
                {
                    string temp = string.Empty;

                    for (int i = 0; i < RandomedDigits.Length; i++)
                        temp += RandomedDigits[i].ToString();

                    randomed_ = Convert.ToUInt32(temp);
                }

                return randomed_;
            }
        }

        private uint randomed_ = 0;

        private List<byte> _availableDigits;
        private Random _random;

        public NumberGenerator(int digitCount = 4)
        {
            DigitCount = digitCount;
            RandomedDigits = new byte[DigitCount];
            _random = new Random();
        }

        public void GenerateNumberWithUniqueDigits()
        {
            randomed_ = 0;
            _availableDigits = GetAvailableDigits();
            RandomedDigits[0] = Convert.ToByte(GetAndExcludeRandomDigit(1));

            for (int i = 1; i < DigitCount; i++)
            {
                RandomedDigits[i] = GetAndExcludeRandomDigit();
            }
        }

        private static List<byte> GetAvailableDigits()
        {
            return new List<byte>()
            {
                0, 1, 2, 3, 4, 5, 6, 7, 8, 9
            };
        }

        private byte GetAndExcludeRandomDigit(int minIndex = 0)
        {
            int index = _random.Next(minIndex, _availableDigits.Count);
            byte toReturn = _availableDigits[index];

            _availableDigits.RemoveAt(index);
            return toReturn;
        }
    }
}
