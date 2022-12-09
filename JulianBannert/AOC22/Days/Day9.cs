using AOC22.Misc;

namespace AOC22.Days;

[Day(9)]
public class Day9 : IDailyPuzzle
{
    public void Run(IEnumerable<string> inputLines)
    {
        var lines = inputLines.ToArray();
        Console.WriteLine("\nRunning Day 9:");
        var part1 = SimulateRopeMovement(lines, 2);
        Console.WriteLine($"Part 1: {part1}");
        var part2 = SimulateRopeMovement(lines, 10);
        Console.WriteLine($"Part 2: {part2}");
    }

    private static int SimulateRopeMovement(IEnumerable<string> inputLines, int knotCount)
    {
        var visitedByTail = new List<GridPosition>();
        var knotPositions = new GridPosition[knotCount];
        foreach (var line in inputLines)
        {
            var s = line.Split(" ");
            for (var step = 0; step < int.Parse(s[1]); step++)
            {
                knotPositions[0] = Move(knotPositions[0], s[0]);
                for (var knotIndex = 0; knotIndex < knotCount-1; knotIndex++) 
                    FollowKnot(knotPositions[knotIndex], ref knotPositions[knotIndex + 1]);
                visitedByTail.Add(knotPositions[knotCount - 1]);
            }
        }
        return visitedByTail.Distinct().Count();
    }

    private static void FollowKnot(GridPosition frontKnot, ref GridPosition followingKnot)
    {
        var xDiff = followingKnot.X - frontKnot.X;
        var yDiff = followingKnot.Y - frontKnot.Y;

        if (yDiff < -1 && xDiff <= -1 || xDiff < -1 && yDiff <= -1) { followingKnot = Move(followingKnot, "RU"); return; }
        if (yDiff < -1 && xDiff >=  1 || xDiff >  1 && yDiff <= -1) { followingKnot = Move(followingKnot, "LU"); return; }
        if (yDiff >  1 && xDiff <= -1 || xDiff < -1 && yDiff >=  1) { followingKnot = Move(followingKnot, "RD"); return; }
        if (yDiff >  1 && xDiff >=  1 || xDiff >  1 && yDiff >=  1) { followingKnot = Move(followingKnot, "LD"); return; }
        if (xDiff >  1) { followingKnot = Move(followingKnot, "L"); return; }
        if (xDiff < -1) { followingKnot = Move(followingKnot, "R"); return; }
        if (yDiff >  1) { followingKnot = Move(followingKnot, "D"); return; }
        if (yDiff < -1) { followingKnot = Move(followingKnot, "U"); return; }
    }

    private static GridPosition Move(GridPosition pos, string dir)
    {
        return dir switch
        {
            "RU" => new GridPosition(pos.X+1, pos.Y+1),
            "RD" => new GridPosition(pos.X+1, pos.Y-1),
            "LU" => new GridPosition(pos.X-1, pos.Y+1),
            "LD" => new GridPosition(pos.X-1, pos.Y-1),
            "L"  => new GridPosition(pos.X-1, pos.Y),
            "R"  => new GridPosition(pos.X+1, pos.Y),
            "D"  => new GridPosition(pos.X, pos.Y-1),
            "U"  => new GridPosition(pos.X, pos.Y+1),
            _ => pos
        };
    }

    private readonly struct GridPosition
    {
        public readonly int X;
        public readonly int Y;

        public GridPosition(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}