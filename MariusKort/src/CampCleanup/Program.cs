static bool HasOverlap((int min, int max) left, (int min, int max) right)
{
    var min = Math.Min(left.min, right.min);
    var max = Math.Max(left.max, right.max);
    return (min == left.min && max == left.max) 
        || (min == right.min && max == right.max);
}

static bool HasPartialOverlap((int min, int max) left, (int min, int max) right)
{
    var min = Math.Min(left.min, right.min);
    var max = Math.Max(left.max, right.max);
    return (min == left.min && left.max >= right.min) 
        || (min == right.min && right.max >= left.min);
}

var lines = await File.ReadAllLinesAsync("input.txt");

var pairs = lines
    .Select(line => line.Split(",")
        .Select(pair => pair.Split("-"))
        .Select(values => (
            min: int.TryParse(values.First(), out var left) ? left : 0,
            max: int.TryParse(values.Last(), out var right) ? right : 0)
        ))
    .ToList();

var overlaps = pairs.Where(line => HasOverlap(line.First(), line.Last()));
var partialOverlaps = pairs.Where(line => HasPartialOverlap(line.First(), line.Last()));

Console.WriteLine($"Total # of overlaps: {overlaps.Count()}");
Console.WriteLine($"Total # of partial overlaps: {partialOverlaps.Count()}");
