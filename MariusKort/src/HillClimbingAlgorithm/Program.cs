using System.Numerics;
using System.Text;

internal class Program
{
    const char MAX_ELEVATION = 'z';
    const char FINISH = 'E';
    const char START = 'S';

    private static readonly IEnumerable<Directions> AllDirections = Enum.GetValues(typeof(Directions)).Cast<Directions>();

    private static async Task Main(string[] _)
    {
        var input = await File.ReadAllLinesAsync("input.txt");
        var lineLength = input.First().Length;
        var grid = string.Join(null, input);

        var startPos = grid.IndexOf(START);
        var endPos = grid.IndexOf(FINISH);
        //Console.ReadKey();
        var res = FindPath(grid.ToCharArray(), lineLength, startPos, endPos);
        Console.WriteLine(res);
    }

    private static int FindPath(char[] grid, int gridWidth, int startPos, int endPos)
    {
        var closed = new HashSet<int>();
        var open = new Queue<Node>();

        var start = new Node(startPos, 0, GetDistance(startPos, endPos, gridWidth));
        open.Enqueue(start);

        while (open.Count > 0)
        {
            var current = open.Dequeue();
            foreach (var direction in AllDirections)
            {
                //Render(grid, visited, lineLength);
                var nextPos = GetNextPosition(direction, current.Position, gridWidth, grid.Length);
                // out of bounds
                if (nextPos < 0)
                {
                    continue;
                }

                //already visited
                if (closed.Contains(nextPos))
                {
                    continue;
                }
                // reached goal
                if (grid[nextPos] == FINISH && grid[current.Position] >= MAX_ELEVATION - 1)
                {
                    return 0;
                }
                // you shall not pass
                if (grid[current.Position] != START && grid[nextPos] != grid[current.Position] && grid[nextPos] != grid[current.Position] + 1)
                {
                    closed.Add(nextPos);
                    continue;
                }

                open.Enqueue(new Node(nextPos, GetDistance(nextPos, startPos, gridWidth), GetDistance(nextPos, endPos, gridWidth)));
            }
            Render(grid, current.Position, open.Select(x => x.Position).ToHashSet(), gridWidth);
        }
        return -1;
    }

    private static int GetDistance(int a, int b, int gridWidth)
    {
        var hDistance = Math.Abs(a - b);
        var vDistance = Math.Abs((a / gridWidth) - (b / gridWidth));
        return hDistance + vDistance;
    }

    private static int GetNextPosition(Directions direction, int currentPos, int gridWidth, int gridSize)
    {
        int nextPos;
        switch (direction)
        {
            case Directions.Up:
                nextPos = currentPos - gridWidth;
                // upper edge
                if (nextPos < 0)
                {
                    return -1;
                }
                break;
            case Directions.Right:
                nextPos = currentPos + 1;
                // right edge
                if (nextPos % gridWidth == 0)
                {
                    return -1;
                }
                break;
            case Directions.Down:
                nextPos = currentPos + gridWidth;
                // lower edge
                if (nextPos >= gridSize)
                {
                    return -1;
                }
                break;
            case Directions.Left:
                // left edge
                if (currentPos % gridWidth == 0)
                {
                    return -1;
                }
                nextPos = currentPos - 1;
                break;
            default:
                return -1;
        }
        return nextPos;
    }


    public static void Render(char[] grid, int current, HashSet<int> open, int lineLength)
    {
        Console.CursorVisible = false;
        Console.SetCursorPosition(0, 0);
        var sb = new StringBuilder();
        for (int i = 0; i < grid.Length; i++)
        {
            if (i % lineLength == 0)
            {
                sb.AppendLine();
            }
            sb.Append(i == current ? "+" : open.Contains(i) ? "*" : ".");
        }
        Console.Write(sb.ToString());
        Console.ReadKey();
    }
}


struct Node
{
    public Node(int position, int g, int h)
    {
        Position = position;
        G = g;
        H = h;
    }

    public int Position { get; init; }
    public int G { get; init; }
    public int H { get; init; }
    public int F => G + H;
}

enum Directions
{
    Up,
    Right,
    Down,
    Left
}