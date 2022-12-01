using AOC22.Misc;

namespace AOC22.Days;

[Day(1)]
public class Day1 : IDailyPuzzle
{
    public void Run(IEnumerable<string> inputLines)
    {
        Console.WriteLine("Running Day 1:");
        var elveSections = string.Join("\n", inputLines).Split("\n\n");
        var result1 = elveSections.Max(s => s.Split("\n").Sum(int.Parse));
        Console.WriteLine($"Part 1: {result1}");
        
        var result2 = elveSections
            .OrderByDescending(s => s.Split("\n").Sum(int.Parse))
            .Take(3)
            .Sum(x => x.Split("\n").Sum(int.Parse));
        
        Console.WriteLine($"Part 2: {result2}");
    }
}