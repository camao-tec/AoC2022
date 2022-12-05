using AOC22.Misc;

namespace AOC22.Days;

[Day(5)]
public class Day5 : IDailyPuzzle
{
    public void Run(IEnumerable<string> inputLines)
    {
        Console.WriteLine("\nRunning Day 5:");
        var sections = string.Join("\n", inputLines).Split("\n\n");
        
        var stacks = ParseStackInfo(sections[0]);
        RunInstructions9000(sections[1], stacks);
        var part1 = string.Join("", stacks.Select(s => s.Peek()));
        Console.WriteLine($"Part 1: {part1}");
        
        stacks = ParseStackInfo(sections[0]);
        RunInstructions9001(sections[1], stacks);
        var part2 = string.Join("", stacks.Select(s => s.Peek()));
        Console.WriteLine($"Part 2: {part2}");
    }

    private void RunInstructions9001(string instructionSection, List<Stack<string>> stacks)
    {
        var lines = instructionSection.Split("\n");
        foreach (var line in lines)
        {
            var splits = line.Split(" ");
            var count = int.Parse(splits[1]);
            var source = int.Parse(splits[3]) -1;
            var target = int.Parse(splits[5]) -1;

            var movedCrates = new List<string>();
            for (var i = 0; i < count; i++) movedCrates.Add(stacks[source].Pop());
            for (var i = movedCrates.Count - 1; i >= 0; i--) stacks[target].Push(movedCrates[i]);
        }
    }

    private void RunInstructions9000(string instructionSection, List<Stack<string>> stacks)
    {
        var lines = instructionSection.Split("\n");
        foreach (var line in lines)
        {
            var splits = line.Split(" ");
            var count = int.Parse(splits[1]);
            var source = int.Parse(splits[3]) -1;
            var target = int.Parse(splits[5]) -1;
            for (var i = 0; i < count; i++) 
                stacks[target].Push(stacks[source].Pop());
        }
    }

    private List<Stack<string>> ParseStackInfo(string stackInfo)
    {
        var lines = stackInfo.Split("\n");
        var stacks = lines[^1]
            .Split("   ")
            .Select(split => new Stack<string>())
            .ToList();

        for (var index = lines.Length-2; index >= 0; index--)
        {
            var line = lines[index];
            var crates = line.Chunk(4).Select(x => x[1].ToString()).ToArray();
            for (var j = 0; j < crates.Length; j++)
            {
                var crate = crates[j];
                if (string.IsNullOrWhiteSpace(crate))
                    continue;
                
                stacks[j].Push(crate);
            }
        }

        return stacks;
    }
}