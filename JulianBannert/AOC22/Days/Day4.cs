using AOC22.Misc;

namespace AOC22.Days;

[Day(4)]
public class Day4 : IDailyPuzzle
{
    public void Run(IEnumerable<string> inputLines)
    {
        Console.WriteLine("\nRunning Day 4:");
        var lines = inputLines.ToArray();
        var part1 = lines.Count(l => RangeIsFullyContained(ParseRanges(l)));
        Console.WriteLine($"Part 1: {part1}");
        var part2 = lines.Count(l => RangesHaveOverlap(ParseRanges(l)));
        Console.WriteLine($"Part 2: {part2}");
    }

    private static bool RangesHaveOverlap(Range[] ranges)
    {
        var r1 = ranges[0];
        var r2 = ranges[1];
        var e1 = Enumerable.Range(r1.Start.Value, r1.End.Value-r1.Start.Value+1).ToArray();
        var e2 = Enumerable.Range(r2.Start.Value, r2.End.Value-r2.Start.Value+1).ToArray();
        return e1.Any(e2.Contains) || e2.Any(e1.Contains);
    }

    private static bool RangeIsFullyContained(Range[] ranges)
    {
        var r1 = ranges[0];
        var r2 = ranges[1];
        var e1 = Enumerable.Range(r1.Start.Value, r1.End.Value-r1.Start.Value+1).ToArray();
        var e2 = Enumerable.Range(r2.Start.Value, r2.End.Value-r2.Start.Value+1).ToArray();
        return (e1.Contains(r2.Start.Value) && e1.Contains(r2.End.Value)) || (e2.Contains(r1.Start.Value) && e2.Contains(r1.End.Value));
    }

    private static Range[] ParseRanges(string line)
    {
        var split = line.Split(",");
        var split1 = split[0].Split("-");
        var split2 = split[1].Split("-");

        return new[]{ new Range(int.Parse(split1[0]), int.Parse(split1[1])),
            new Range(int.Parse(split2[0]), int.Parse(split2[1]))};
    }
}