var lines = await File.ReadAllLinesAsync("input.txt");

var left = new List<string> { "A", "B", "C" };
var right = new List<string> { "X", "Y", "Z" };

var pointsLookup = new[] { 4, 8, 3, 1, 5, 9, 7, 2, 6 };
var pointsLookup2 = new[] { 3, 4, 8, 1, 5, 9, 2, 6, 7 };

var points = lines
    .Select(line => line.Split(" "))
    .Select(x => (left: left.IndexOf(x.First()), right: right.IndexOf(x.Last())))
    .ToList();

var sum = points.Select(x => pointsLookup[(x.left * left.Count) + x.right]).Sum();
var sum2 = points.Select(x => pointsLookup2[(x.left * left.Count) + x.right]).Sum();

Console.WriteLine($"First Result: {sum}");
Console.WriteLine($"Second Result: {sum2}");
