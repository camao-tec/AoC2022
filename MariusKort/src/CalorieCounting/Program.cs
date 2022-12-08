var input = await File.ReadAllTextAsync("input.txt");

var elves = input
    .Split(Environment.NewLine + Environment.NewLine)
    .Select(x => x
        .Split(Environment.NewLine)
        .Select(y => int.TryParse(y, out var res) ? res : 0)
        .Aggregate((y, z) => y + z)
    )
    .OrderByDescending(x => x)
    .ToList();

Console.WriteLine(elves.First());
Console.WriteLine(elves.Take(3).Sum());