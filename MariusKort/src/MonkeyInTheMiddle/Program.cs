internal class Program
{
    private static async Task Main(string[] _)
    {
        var input = await File.ReadAllTextAsync("input.txt");
        Console.WriteLine($"Part 1: {Process(input, 20)}");
        Console.WriteLine($"Part 2: {Process(input, 10000, false)}");
    }

    private static long Process(string input, int numOfRounds, bool useFixedDivisor = true)
    {
        var monkeys = input
                    .Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Split(Environment.NewLine))
                    .Select(x => new Monkey(x))
                    .ToArray();

        for (int i = 0; i < numOfRounds; i++)
        {
            foreach (var monkey in monkeys)
            {
                monkey.TakeTurn(ref monkeys, useFixedDivisor);
            }
        }

        var topMonkeys = monkeys.Select(m => m.InspectCounter).OrderByDescending(x => x).Take(2).ToArray();
        return topMonkeys[0] * topMonkeys[1];
    }
}
