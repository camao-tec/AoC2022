using System.Numerics;
using System.Text;

internal class Program
{
    const char MIN_ELEVATION = 'a';
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
        // Console.ReadKey();

        var startPos2 = new List<int>();
        for (int i = grid.IndexOf('a'); i > -1; i = grid.IndexOf('a', i + 1))
        {
            if (i >= 0)
                startPos2.Add(i);
        }

        var result2 = startPos2.Select(x =>
        {
            var res = FindPath(grid.ToCharArray(), lineLength, x, endPos);
            var path = GetNodes(res);
            return path;
        })
        .Where(x => x.Count > 0)
        .ToList();

        // result2.ForEach(path => Render(grid.ToCharArray(), path, lineLength, startPos, endPos));
        var path = result2.MinBy(x => x.Count);
        Render(grid.ToCharArray(), path, lineLength, path.First().Position, endPos);

        // var res = FindPath(grid.ToCharArray(), lineLength, startPos, endPos);
        // var path = GetNodes(res);
        Console.WriteLine();
        // Console.WriteLine(result2);
    }

    private static LinkedList<Node> GetNodes(Node? start)
    {
        var res = new LinkedList<Node>();
        while (start != null)
        {
            res.AddFirst(start);
            start = start.Previous;
        }
        return res;
    }

    private static Node? FindPath(char[] grid, int gridWidth, int startPos, int endPos)
    {
        var closed = new HashSet<int>();
        var open = new Queue<Node>();

        var start = new Node(startPos, 0, GetDistance(startPos, endPos, gridWidth), MIN_ELEVATION);
        open.Enqueue(start);

        while (open.Count > 0)
        {
            open = new Queue<Node>(open.OrderBy(x => x.F));
            var current = open.Dequeue();

            // Render(grid, current.Position, open.Select(x => x.Position).ToHashSet(), closed, gridWidth);

            closed.Add(current.Position);
            foreach (var direction in AllDirections)
            {
                var nextPos = GetNextPosition(direction, current.Position, gridWidth, grid.Length);
                // out of bounds
                if (nextPos < 0)
                {
                    continue;
                }
                // reached goal
                if (grid[nextPos] == FINISH && grid[current.Position] >= MAX_ELEVATION - 1)
                {
                    return current;
                }
                //already visited
                if (closed.Contains(nextPos))
                {
                    continue;
                }
                // you shall not pass
                if (grid[current.Position] != START && grid[nextPos] > grid[current.Position] + 1)
                {
                    // closed.Add(nextPos);
                    continue;
                }

                Node item = new(nextPos, GetDistance(nextPos, startPos, gridWidth), GetDistance(nextPos, endPos, gridWidth), grid[nextPos], current);
                if (open.Contains(item))
                {
                    continue;
                }
                open.Enqueue(item);
            }
        }
        return null;
    }

    private static int GetDistance(int a, int b, int gridWidth)
    {
        var hDistance = Math.Abs((a % gridWidth) - (b % gridWidth));
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
    public static void Render(char[] grid, LinkedList<Node> nodes, int gridWidth, int startPos, int endPos)
    {
        Console.CursorVisible = false;
        Console.SetWindowSize(gridWidth + 1, 50);
        Console.SetCursorPosition(0, 0);
        var sb = new StringBuilder();
        for (int i = 0; i < grid.Length; i++)
        {
            if (i % gridWidth == 0)
            {
                sb.AppendLine();
            }
            if (nodes.Contains(new Node(i, 0, 0, ' ')) || i == startPos || i == endPos)
            {
                sb.Append(grid[i]);
            }
            else
            {
                sb.Append(".");
            }

        }
        Console.Write(sb.ToString());
        // Console.ReadKey();
    }

    public static void Render(char[] grid, int current, HashSet<int> open, HashSet<int> closed, int gridWidth)
    {
        Console.CursorVisible = false;
        Console.SetWindowSize(gridWidth + 1, 50);
        Console.SetCursorPosition(0, 0);
        var sb = new StringBuilder();
        for (int i = 0; i < grid.Length; i++)
        {
            if (i % gridWidth == 0)
            {
                sb.AppendLine();
            }
            if (open.Contains(i))
            {
                sb.Append(grid[i]);
            }
            else if (i == current)
            {
                sb.Append("+");
            }
            else
            {
                sb.Append(".");
            }

        }
        Console.Write(sb.ToString());
        Console.WriteLine();
        Console.WriteLine($"Current Elevation: {grid[current]}");
        // Console.ReadKey();
    }
}


class Node
{
    public Node(int position, int g, int h, char elevation, Node? previous = null)
    {
        Position = position;
        G = g;
        H = h;
        Elevation = elevation;
        Previous = previous;
    }

    public int Position { get; init; }
    public int G { get; init; }
    public int H { get; init; }
    public char Elevation { get; }
    public Node? Previous { get; init; }

    public double F => (G + H) / Elevation;

    public override bool Equals(object? obj)
    {
        return obj is Node node &&
               Position == node.Position;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Position, G, H, F);
    }
}

enum Directions
{
    Up,
    Right,
    Down,
    Left
}
