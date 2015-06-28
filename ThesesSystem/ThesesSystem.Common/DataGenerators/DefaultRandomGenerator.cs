namespace ThesesSystem.Common.DataGenerators
{
    using System;

    public class DefaultRandomGenerator : IRandomGenerator
    {
         private const int MaxDays = 3651;
        private static DefaultRandomGenerator instance;
        private Random generator;
        private string letters = "абвгдежзийклмнопрстуфхцчшщъьюяabcdefghijklmnopqrstuvwxyz";

        private DefaultRandomGenerator()
        {
            this.generator = new Random();
        }

        public static DefaultRandomGenerator Instance
        {
            get
            {
                if (DefaultRandomGenerator.instance == null)
                {
                    DefaultRandomGenerator.instance = new DefaultRandomGenerator();
                }

                return DefaultRandomGenerator.instance;
            }
        }

        public int GenerateNumber(int from = 0, int to = int.MaxValue)
        {
            to = to == int.MaxValue ? to : to + 1;
            var randomNumber = this.generator.Next(from, to);

            return randomNumber;
        }

        public DateTime GenerateDate(DateTime after)
        {
            int days = this.GenerateNumber(0, MaxDays);
            return after.AddDays(days);
        }

        public double GenerateRealNumber(double from = 0, double to = int.MaxValue)
        {
            var randomNumber = this.generator.NextDouble() * (to - from) + from;

            return randomNumber;
        }

        public string GenerateString(int minLength, int maxLenght)
        {
            var randomLength = this.GenerateNumber(minLength, maxLenght);
            var randomString = this.GenerateStirngWithGivenLength(randomLength);

            return randomString;
        }

        private string GenerateStirngWithGivenLength(int length)
        {
            var word = new char[length];
            var lettersLength = this.letters.Length;

            for (int index = 0; index < length; index++)
            {
                word[index] = this.letters[this.generator.Next(lettersLength)];
            }

            return new string(word);
        }
    }
}
