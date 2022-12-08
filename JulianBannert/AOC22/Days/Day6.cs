using AOC22.Misc;

namespace AOC22.Days;

[Day(6)]
public class Day6 : IDailyPuzzle
{
    public void Run(IEnumerable<string> inputLines)
    {
        Console.WriteLine("\nRunning Day 6:");
        var line = inputLines.First();
        Console.WriteLine($"Part 1: {GetOffsetMarker(line, 4)}");
        Console.WriteLine($"Part 2: {GetOffsetMarker(line, 14)}");
    }

    private int GetOffsetMarker(string line, int distinctCount)
    {
        var q = new Queue<char>(distinctCount);
        for (var i = 0; i < distinctCount; i++) q.Enqueue(line[i]);
        var offset = distinctCount;
        while (offset < line.Length)
        {
            if (!q.GroupBy(x => x).Any(x => x.Count() > 1)) break;
            q.Dequeue();
            q.Enqueue(line[offset]);
            offset++;
        }
        return offset;
    }
}
