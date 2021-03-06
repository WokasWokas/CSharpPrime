using System.Security.Cryptography;
using System;

namespace KolattseHypotisys
{
    class Program
    {
        static void Main(string[] args)
        {
            Engine engine = new Engine();
            engine.GetRandomPrimeNumbers(2, 32, out long[] arr);
            foreach (long num in arr)
            {
                Console.WriteLine(num);
            }
        }
    }
    class Engine
    {
        private int[] PrimeNumbers;
        public Engine()
        {
            GetPrimeNumberUpTo(1101);
        }
        private void RandomBytes(int lenght, out byte[] bytes)
        {
            byte[] _bytes = new byte[lenght];
            _bytes[0] = 1;
            for (int i = 1; i < lenght; i++)
            {
                _bytes[i] = (byte)RandomNumberGenerator.GetInt32(0, 8);
            }
            bytes = _bytes;
        }
        private void GetPrimeNumberUpTo(int ArrayLenght)
        {
            int[] _PrimeNumbers = new int[ArrayLenght];
            int lastj = 2;
            for (int i = 0; i < ArrayLenght; i++)
            {
                for (int j = lastj; j < 10000000; j++)
                {
                    bool _status = true;
                    if (j == 2)
                        _PrimeNumbers[i] = j;
                    for (int z = 2; z < j; z++)
                    {
                        if (j % z == 0)
                        {
                            _status = false;
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    if (_status)
                    {
                        _PrimeNumbers[i] = j;
                        lastj = j + 1;
                        break;
                    }
                }
            }
            PrimeNumbers = _PrimeNumbers;
        }
        private bool IsPrimeNumber(long number)
        {
            if (number <= 1)
                return false;
            else if (number <= 3)
                return true;
            else if (number % 2 == 0 | number % 3 == 0)
                return false;
            foreach (int num in PrimeNumbers)
            {
                if (number == num)
                    return true;
                if (number % num == 0)
                    return false;
            }
            long i = 5;
            while (i*i <= number)
            {
                if (number % i == 0 | number % (i + 2) == 0)
                    return false;
                i += 6;
            }
            return true;
        }
        private long TryGetRandomPrimeNumber(int ByteLenght)
        {
            while (true)
            {
                RandomBytes(ByteLenght, out byte[] Bytes);
                long BytesValue = BitConverter.ToInt32(Bytes);
                if (IsPrimeNumber(BytesValue))
                    return BytesValue;
            }
        }
        public void GetRandomPrimeNumbers(int Quantity, int ByteLenght, out long[] PrimeNumbers)
        {
            long[] _PrimeNumbers = new long[Quantity];
            for (int i = 0; i < Quantity; i++)
            {
                _PrimeNumbers[i] = TryGetRandomPrimeNumber(ByteLenght);
            }
            PrimeNumbers = _PrimeNumbers;
        }
    }
}
