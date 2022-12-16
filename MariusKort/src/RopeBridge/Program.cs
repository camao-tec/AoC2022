using System.Numerics;
using System.Text;

var input = await File.ReadAllLinesAsync("input.txt");

var directions = input
    .Select(s => (direction: GetDirection(s.First()), steps: byte.TryParse(s[2..], out var count) ? count : 0))
    .ToList();

//var ropeOne = new Vector2[2].ToList();
//var tailOnePositions = Run(directions, ropeOne);
//Console.WriteLine($"Number of positions part 1: {tailOnePositions.Count}");

var ropeTwo = new Vector2[10].ToList();
var tailTwoPositions = Run(directions, ropeTwo);
Console.WriteLine($"Number of positions part 2: {tailTwoPositions.Count}");

static List<Vector2> Run(List<(Vector2 direction, int steps)> directions, List<Vector2> rope)
{
    var tailPositions = new List<Vector2>() { Vector2.Zero };
    for (int i = 0; i < directions.Count; i++)
    {
        (Vector2 direction, int steps) = directions[i];
        for (int _ = 0; _ < steps; _++)
        {
            Update(rope, direction);
            if (!tailPositions.Contains(rope.Last()))
            {
                tailPositions.Add(rope.Last());
            }
        }
    }
    return tailPositions;
}

static void Update(List<Vector2> rope, Vector2 direction)
{
    Render(rope);
    var previousPos = rope[0];
    rope[0] += direction;
    for (int i = 0; i < rope.Count - 1; i++)
    {
        var distance = Vector2.Distance(rope[i], rope[i + 1]);
        if (distance >= 2.0f)
        {
            var dir = rope[i] - rope[i + 1];
            previousPos = rope[i + 1];
            rope[i + 1] += Vector2.Clamp(dir, new Vector2(-1, -1), new Vector2(1, 1));

        }
    }
}

static Vector2 GetDirection(char direction)
{
    return direction switch
    {
        'U' => new Vector2(0, 1),
        'R' => new Vector2(1, 0),
        'D' => new Vector2(0, -1),
        'L' => new Vector2(-1, 0),
        _ => throw new ArgumentException("Unknown direction", nameof(direction))
    };
}

static void Render(List<Vector2> rope)
{
    Console.Clear();
    var sb = new StringBuilder();
    for (int row = 15; row > -15; row--)
    {
        sb.AppendLine();
        for (int col = -25; col < 25; col++)
        {
            var current = new Vector2(col, row);
            var index = rope.IndexOf(current);
            if (index == 0)
            {
                sb.Append("H");
                continue;
            }
            if (index > 0)
            {
                sb.Append(index);
                continue;
            }
            if (index < 0 && current == Vector2.Zero)
            {
                sb.Append("s");
                continue;
            }
            sb.Append(".");
        }
    }
    Console.WriteLine(sb.ToString());
    //Console.ReadKey();
    Thread.Sleep(50);
}

static void RenderHistory(List<Vector2> history)
{
    var sb = new StringBuilder();
    for (int row = 15; row > -15; row--)
    {
        sb.AppendLine();
        for (int col = -25; col < 25; col++)
        {
            var current = new Vector2(col, row);
            if (history.Contains(current))
            {
                sb.Append("#");
                continue;
            }
            if (current == Vector2.Zero)
            {
                sb.Append("s");
            }
            sb.Append(".");
        }
    }
    Console.WriteLine(sb.ToString());
}
