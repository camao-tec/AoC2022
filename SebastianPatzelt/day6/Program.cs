using System.Linq;
using System.IO;
using System;

Console.WriteLine(File.ReadAllText("input.txt").FindIndexOfStartPackage(4));
Console.WriteLine(File.ReadAllText("input.txt").FindIndexOfStartPackage(14));
Console.ReadLine();

public static class SignalParser
{
    public static int FindIndexOfStartPackage(this string signal, int startPackageLength)
    {
        var current = signal.Take(startPackageLength);
        bool found = false;
        var skips = 0;
        while (!found)
        {
            found = current.Distinct().Count() == startPackageLength;
            current = signal.Skip(skips + 1).Take(startPackageLength);
            skips++;
        }
        return skips - 1 + startPackageLength;

    }
}


