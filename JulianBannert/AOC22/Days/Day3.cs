using AOC22.Misc;

namespace AOC22.Days;

[Day(3)]
public class Day3 : IDailyPuzzle
{
    public void Run(IEnumerable<string> inputLines)
    {
        Console.WriteLine("\nRunning Day 3:");
        var lines = inputLines.ToArray();
        var part1 = lines.Sum(line => GetPriority(FindCommonItem(line[..(line.Length /2)], line[(line.Length / 2)..])));
        Console.WriteLine($"Part 1: {part1}");
        var part2 = lines.Chunk(3).Sum(chunk => GetPriority(FindCommonBadge(chunk)));
        Console.WriteLine($"Part 2: {part2}");
    }

    private static char FindCommonBadge(string[] rucksacks) => rucksacks[0].First(c => rucksacks[1].Contains(c) && rucksacks[2].Contains(c));

    private static char FindCommonItem(string compartment1, string compartment2) => compartment1.First(compartment2.Contains);

    private static int GetPriority(char c) => char.IsLower(c) ? (byte) c - 96 : (byte) c - 64 + 26;
}