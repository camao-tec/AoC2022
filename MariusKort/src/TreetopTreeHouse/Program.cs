var input = await File.ReadAllLinesAsync("input.txt");

var grid = new Grid(input);
var visibleTrees = grid.Data
    .Select((_, i) => grid.GetTree(i))
    .Where(t => t.IsVisible)
    .ToList();

//part 1
Console.WriteLine($"visible: {visibleTrees.Count}");
//part 2
Console.WriteLine($"top score: {visibleTrees.Max(x => x.Score)}");

struct Tree
{
    public int Score;
    public bool IsVisible;
}

class Grid
{
    public byte[] Data { get; }
    public int SizeX { get; }
    public int SizeY { get; }

    public byte this[int index]
    {
        get => Data[index];
    }

    public int Length => Data.Length;

    public Grid(string[] input)
    {
        Data = string.Join("", input.Select(x => x.Trim()))
            .Select(c => byte.TryParse(c.ToString(), out var value) ? value : byte.MinValue)
            .ToArray();

        SizeX = input.FirstOrDefault()?.Length ?? 0;
        SizeY = input.Length;
    }

    public Tree GetTree(int index)
    {
        var top = IsVisibleTop(index, out var topScore);
        var right = IsVisibleRight(index, out var rightScore);
        var bottom = IsVisibleBottom(index, out var bottomScore);
        var left = IsVisibleLeft(index, out var leftScore);
        return new Tree
        {
            IsVisible = top || right || bottom || left,
            Score = topScore * rightScore * bottomScore * leftScore
        };
    }

    public bool IsVisibleTop(int index, out int score)
    {
        score = 0;
        var value = Data[index];
        while (index >= SizeX)
        {
            index -= SizeX;
            score++;
            if (value <= Data[index])
            {
                return false;
            }
        }
        return true;
    }

    public bool IsVisibleBottom(int index, out int score)
    {
        score = 0;
        var value = Data[index];
        while (index + SizeX < SizeX * SizeY)
        {
            index += SizeX;
            score++;
            if (value <= Data[index])
            {
                return false;
            }
        }
        return true;
    }

    public bool IsVisibleLeft(int index, out int score)
    {
        score = 0;
        var value = Data[index];
        var boundary = (index / SizeX) * SizeX;
        while ((index - 1) >= boundary)
        {
            index--;
            score++;
            if (value <= Data[index])
            {
                return false;
            }
        }
        return true;
    }

    public bool IsVisibleRight(int index, out int score)
    {
        score = 0;
        var value = Data[index];
        var boundary = ((index / SizeX) + 1) * SizeX;
        while ((index + 1) < boundary)
        {
            index++;
            score++;
            if (value <= Data[index])
            {
                return false;
            }
        }
        return true;
    }

}