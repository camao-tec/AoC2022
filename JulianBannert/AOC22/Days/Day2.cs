using AOC22.Misc;

namespace AOC22.Days;

[Day(2)]
public class Day2 : IDailyPuzzle
{
    private readonly Dictionary<string, int> _letterToPoints = new(){ {"A", 1}, {"B", 2}, {"C", 3}, {"X", 1}, {"Y", 2}, {"Z", 3} };
    private readonly Dictionary<string, int> _letterToResult = new() { { "X", 0 }, { "Y", 3 }, { "Z", 6 } };
    private readonly Dictionary<string, int> _pickForWin = new() { {"A", 2}, {"B", 3}, {"C", 1} };
    private readonly Dictionary<string, int> _pickForDraw = new() { {"A", 1}, {"B", 2}, {"C", 3} };
    private readonly Dictionary<string, int> _pickForLoss = new() { {"A", 3}, {"B", 1}, {"C", 2} };

    public void Run(IEnumerable<string> inputLines)
    {
        Console.WriteLine("\nRunning Day 2:");
        var total = 0;
        var total2 = 0;
        foreach (var round in inputLines)
        {
            var split = round.Split(" ");
            var opp = split[0];
            var you = split[1];
            total += _letterToPoints[you] + PointsForRound(you, opp);
            var res = you;
            total2 += _letterToResult[res] + PickForResult(res, opp);
        }
        Console.WriteLine($"Part 1: {total}");
        Console.WriteLine($"Part 2: {total2}");
    }

    private int PickForResult(string res, string opp) =>
        res switch
        {
            "X" => _pickForLoss[opp],
            "Y" => _pickForDraw[opp],
            "Z" => _pickForWin[opp],
            _ => 0
        };

    private int PointsForRound(string you, string opp)
    {
        if ((you == "X" && opp == "C") || (you == "Y" && opp == "A") || (you == "Z" && opp == "B"))
            return 6;

        if ((opp == "A" && you == "Z") || (opp == "B" && you == "X") || (opp == "C" && you == "Y"))
            return 0;

        return 3;
    }
}