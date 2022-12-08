using AOC22.Misc;

namespace AOC22.Days;

[Day(8)]
public class Day8 : IDailyPuzzle
{
    // i cba to clean this up...
    public void Run(IEnumerable<string> inputLines)
    {
        Console.WriteLine("\nRunning Day 8:");
        var grid = inputLines.ToArray();
        var part1 = 0;
        var part2 = 0;
        for (var rowIndex = 0; rowIndex < grid.Length; rowIndex++)
        {
            var row = grid[rowIndex];
            if (rowIndex == 0 || rowIndex == grid.Length - 1)
            {
                part1 += row.Length;
                continue;
            }

            for (var colIndex = 0; colIndex < row.Length; colIndex++)
            {
                if (colIndex == 0 || colIndex == row.Length - 1)
                {
                    part1++;
                    continue;
                }
                
                if (IsVisible(grid, rowIndex, colIndex)) 
                    part1++;
                var score = CalculateScenicScore(grid, rowIndex, colIndex);
                if (score > part2) 
                    part2 = score;
            }
        }

        Console.WriteLine($"Part 1: {part1}");
        Console.WriteLine($"Part 2: {part2}");
    }
    private bool IsVisible(string[] grid, int row, int col) =>
        IsVisibleFromTop(grid, row, col, out _)
        || IsVisibleFromBottom(grid, row, col, out _)
        || IsVisibleFromLeft(grid, row, col, out _)
        || IsVisibleFromRight(grid, row, col, out _);

    private int CalculateScenicScore(string[] grid, int row, int col)
    {
        IsVisibleFromTop(grid, row, col, out var t);
        IsVisibleFromBottom(grid, row, col, out var b);
        IsVisibleFromLeft(grid, row, col, out var l);
        IsVisibleFromRight(grid, row, col, out var r);
        return t * l * b * r;
    }

    private int CountVisibleTrees(int height, List<int> trees)
    {
        var count = 0;
        foreach (var treeHeight in trees)
        {
            count++;
            if (height <= treeHeight)
                break;
        }

        return count;
    }

    private bool IsVisibleFromTop(string[] grid, int row, int col, out int visibleTreeCount)
    {
        var height = int.Parse(grid[row][col].ToString());
        var list = new List<int>();
        for (var r = row-1; r >= 0; r--) 
            list.Add(int.Parse(grid[r][col].ToString()));
        visibleTreeCount = Math.Max(CountVisibleTrees(height, list), 1);
        if (row == 0) visibleTreeCount = 0;
        return !list.Any(x => x >= height);
    }
    
    private bool IsVisibleFromBottom(string[] grid, int row, int col, out int visibleTreeCount)
    {
        var height = int.Parse(grid[row][col].ToString());
        var list = new List<int>();
        for (var r = row+1; r < grid.Length; r++) 
            list.Add(int.Parse(grid[r][col].ToString()));
        visibleTreeCount = Math.Max(CountVisibleTrees(height, list), 1);
        if (row == grid.Length-1) visibleTreeCount = 0;
        return !list.Any(x => x >= height);
    }
    
    private bool IsVisibleFromLeft(string[] grid, int row, int col, out int visibleTreeCount)
    {
        var height = int.Parse(grid[row][col].ToString());
        var list = new List<int>();
        for (var c = col-1; c >= 0; c--) 
            list.Add(int.Parse(grid[row][c].ToString()));
        visibleTreeCount = Math.Max(CountVisibleTrees(height, list), 1);
        if (col == 0) visibleTreeCount = 0;
        return !list.Any(x => x >= height);
    }
    
    private bool IsVisibleFromRight(string[] grid, int row, int col, out int visibleTreeCount)
    {
        var height = int.Parse(grid[row][col].ToString());
        var list = new List<int>();
        for (var c = col+1; c < grid[row].Length; c++) 
            list.Add(int.Parse(grid[row][c].ToString()));
        visibleTreeCount = Math.Max(CountVisibleTrees(height, list), 1);
        if (col == grid[row].Length-1) visibleTreeCount = 0;
        return !list.Any(x => x >= height);
    }
    
}