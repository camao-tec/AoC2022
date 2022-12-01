using System.Reflection;
using System.Text;

namespace AOC22.Misc;

public static class InputHelper
{
    public static IEnumerable<string> ReadInputLines(int index)
    {
        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"AOC22.Inputs.day{index}.txt");
        if (stream == null)
        {
            Console.WriteLine($"Input for index: {index} could not be found.");
            return Array.Empty<string>();
        }
        using var reader = new StreamReader(stream, Encoding.UTF8);

        var list = new List<string>();
        while (reader.ReadLine() is { } line)
            list.Add(line);

        return list;
    }
}