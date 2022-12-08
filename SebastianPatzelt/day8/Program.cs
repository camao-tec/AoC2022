using System.Linq;
using System.IO;
using System;
using System.Collections.Generic;

var grid = GridParser.Parse(File.ReadAllLines("input.txt"));
var innerTrees = grid.SelectMany(x => x).Count(x => x.Visibility != null && x.Visibility.Visible);
var dimension = grid.Length;
var outerTrees = 2 * dimension + 2 * (dimension - 2);
Console.WriteLine(innerTrees + outerTrees);
Console.WriteLine(grid.SelectMany(x => x).Where(x => x.Visibility != null).Max(x => x.Visibility.Score));
Console.ReadLine();

public static class GridParser
{
    public static Cell[][] Parse(this string[] lines)
    {
        var root = lines.Select((x, row) => x.Select((y, col) => new Cell(row, col, int.Parse(y.ToString()))).ToArray()).ToArray();
        for (int row = 1; row < root.Length - 1; row++)
        {
            for (int cell = 1; cell < root[row].Length - 1; cell++)
            {
                if (row == 3 && cell == 2)
                {
                    var o = "bama";
                }
                if (row == 0 || cell == 0) continue;
                var cellToInspect = root[row][cell];
                var neighBours = GridHelper.GetNeigbours(cellToInspect, root);
                var left = neighBours.Where(n => n.Row == row && n.Col < cellToInspect.Col);
                var right = neighBours.Where(n => n.Row == row && n.Col > cellToInspect.Col);
                var bottom = neighBours.Where(n => n.Row > row && n.Col == cellToInspect.Col);
                var top = neighBours.Where(n => n.Row < row && n.Col == cellToInspect.Col);
                var visibleFromLeft = !left.Any(y => y.Value >= cellToInspect.Value);
                var visibleFromRight = !right.Any(y => y.Value >= cellToInspect.Value);
                var visibleFromTop = !top.Any(y => y.Value >= cellToInspect.Value);
                var visibleFromBottom = !bottom.Any(y => y.Value >= cellToInspect.Value);
                
                cellToInspect.Visibility = new Visibility(visibleFromLeft, visibleFromRight, visibleFromTop, visibleFromBottom);
                int leftScore = GetScore(cellToInspect, left, true);
                int rightScore = GetScore(cellToInspect, right, false);
                int topScore = GetScore(cellToInspect, top, true);
                int downScore = GetScore(cellToInspect, bottom, false);
                cellToInspect.Visibility.Score = leftScore * rightScore * topScore * downScore;
            }
        }
        return root;
    }

    public static int GetScore(Cell cellToInspect, IEnumerable<Cell> line, bool reverse)
    {
        if (reverse)
        {
            line = line.Reverse();
        }
        var valued = line.Select((x, i) => new { Blocked = x.Value >= cellToInspect.Value, Score = i + 1 }).ToArray();
        var visible = 0;
        foreach (var neighbor in valued)
        {
            if (neighbor.Blocked)
            {
                visible++;
                break;
            }
            visible++;
        }
        return visible;
    }
}

public record Cell(int Row, int Col, int Value)
{
    public Visibility Visibility { get; set; }
}

public record Visibility(bool Left, bool Right, bool Top, bool Bottom)
{
    public bool Visible => Left || Right || Top || Bottom;
    public long Score { get; set; }

}

public static class GridHelper
{
    public static IEnumerable<Cell> GetNeigbours(Cell cellToInspect, Cell[][] grid)
    {
        var allOfCol = grid.SelectMany(x => x).Where(x => x.Col == cellToInspect.Col);
        return grid[cellToInspect.Row].Where(x => x.Col != cellToInspect.Col).Concat(allOfCol);
    }
}