namespace ThesesSystem.Common.DataGenerators
{
    using System;

    public interface IRandomGenerator
    {
        int GenerateNumber(int from, int to);

        double GenerateRealNumber(double from, double to);

        string GenerateString(int minLength, int maxLenght);

        DateTime GenerateDate(DateTime after);
    }
}
