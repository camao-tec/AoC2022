using System.Text.RegularExpressions;

internal class Monkey
{
    private const byte INSTRUCTIONS_LINE_COUNT = 6;
    private const byte ROUND_DIVISOR = 3;

    private static readonly Regex numberRegex = new("[0-9]+");
    private static readonly Regex operationRegex = new("old ([\\*\\+\\/\\-]{1}) ([0-9]+)?");

    private readonly Func<int, bool> _test;

    private readonly Queue<int> _startingItems;
    private readonly Func<long, long> _operation;
    private readonly Action<int, bool, Monkey[]> _throwItem;

    private static long? modulo = null;

    public int TestDivisor { get; private set; }
    public long InspectCounter { get; private set; }

    public Monkey(string[] instructions)
    {
        if (instructions == null || instructions.Length < INSTRUCTIONS_LINE_COUNT)
        {
            throw new ArgumentException("invalid instructions", nameof(instructions));
        }

        _startingItems = InitStartingItems(instructions[1]);
        _operation = InitOperation(instructions[2]);
        TestDivisor = InitTestDivisor(instructions[3]);
        _throwItem = InitThrowItem(instructions[4], instructions[5]);
        _test = (x) => x % TestDivisor == 0;
    }

    public void TakeTurn(ref Monkey[] monkeys, bool useFixedDivisor = true)
    {
        while (_startingItems.Count > 0)
        {
            long item = _startingItems.Dequeue();
            InspectCounter++;
            item = _operation.Invoke(item);
            if (useFixedDivisor)
            {
                item /= ROUND_DIVISOR;
            }
            else
            {
                modulo ??= monkeys.Select(x => x.TestDivisor).Aggregate((x, y) => x * y);
                item %= modulo.Value;
            }

            var result = _test.Invoke((int)item);
            _throwItem.Invoke((int)item, result, monkeys);
        }
    }

    public void CatchItem(int item)
    {
        _startingItems.Enqueue(item);
    }

    private static Action<int, bool, Monkey[]> InitThrowItem(string trueString, string falseString)
    {
        var trueIndex = byte.TryParse(numberRegex.Match(trueString)?.Value, out var trueIndexValue)
            ? trueIndexValue
            : throw new ArgumentException("failed to parse true index", nameof(trueString));

        var falseIndex = byte.TryParse(numberRegex.Match(falseString)?.Value, out var falseIndexValue)
            ? falseIndexValue
            : throw new ArgumentException("failed to parse false index", nameof(falseString));

        return (x, result, monkeys) =>
        {
            if (result)
            {
                monkeys[trueIndex].CatchItem(x);
                return;
            }
            monkeys[falseIndex].CatchItem(x);
        };
    }

    private static byte InitTestDivisor(string input)
    {
        var match = numberRegex.Match(input);
        if (!match.Success || !byte.TryParse(match.Value, out var divisor))
        {
            throw new FormatException("failed to parse test line");
        }
        return divisor;
    }

    private static Func<long, long> InitOperation(string input)
    {
        Match match = operationRegex.Match(input);
        if (!match.Success || match.Groups.Count < 2)
        {
            throw new ArgumentException("failed to parse operation", nameof(input));
        }

        long? rightOperand = match.Groups.Count > 2
            ? long.TryParse(match.Groups[2].Value, out var operand) ? operand : null
            : null;

        return match.Groups[1].Value switch
        {
            "+" => (x) => x + (rightOperand ?? x),
            "-" => (x) => x - (rightOperand ?? x),
            "*" => (x) => x * (rightOperand ?? x),
            "/" => (x) => x / (rightOperand ?? x),
            _ => throw new ArgumentException("failed to parse operator", nameof(input)),
        };
    }

    private static Queue<int> InitStartingItems(string input)
    {
        var items = numberRegex.Matches(input).Select(m => int.TryParse(m.Value, out var value) ? value : -1).Where(i => i > 0);
        return new Queue<int>(items ?? Enumerable.Empty<int>());
    }
}