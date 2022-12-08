var input = await File.ReadAllLinesAsync("input.txt");

const int UPPER_OFFSET = 38;
const int LOWER_OFFSET = 96;

static int GetValue(char item)
{
    return item - (char.IsUpper(item) ? UPPER_OFFSET : LOWER_OFFSET);
}

// part 1
const int COMPARTMENT_SIZE = 2;

var priorities = input
    .Select(line => line
        .Select((item, index) => (item, index))
        .GroupBy(x => x.item)
        .Where(x => x.Any(y => y.index < line.Length / COMPARTMENT_SIZE) && x.Any(z => z.index >= line.Length / COMPARTMENT_SIZE))
        .Select(x => (item: x.Key, value: GetValue(x.Key))))
    .ToList();

var sum = priorities
    .SelectMany(x => x.Select(y => y.value))
    .Sum();

Console.WriteLine(sum);

// part 2
const int GROUP_SIZE = 3;

var priorities2 = input
    .Select((lines, index) => (
        items: lines.Select(item => (item, index)),
        group: index / GROUP_SIZE
    ))
    .GroupBy(x => x.group)
    .Select(group => group
        .SelectMany(x => x.items)
        .ToLookup(x => x.item, y => y.index)
        .Where(x => x.Distinct().Count() == GROUP_SIZE))
    .SelectMany(group => group.Select(x => (item: x.Key, value: GetValue(x.Key))));

var sum2 = priorities2.Select(y => y.value).Sum();
Console.WriteLine(sum2);
