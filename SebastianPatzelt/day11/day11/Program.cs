
var monkeys = MonkeyParser.Parse(File.ReadAllLines("input.txt"));
for (int i = 0; i < 20; i++)
{
    MonkeyParser.Inspect(monkeys);
}
var busiest = monkeys.OrderByDescending(x => x.InspectionCount).Distinct().Take(2);
var monkeyBusiness = busiest.First().InspectionCount * busiest.Last().InspectionCount;
Console.WriteLine(monkeyBusiness);


public static class MonkeyParser
{
    public static void Inspect(List<Monkey> monkeys)
    {
        foreach (var monkey in monkeys)
        {
            while (monkey.Items.Any())
            {
                monkey.InspectionCount++;
                var item = monkey.Items.Dequeue();
                int inspectedItem = 0;
                if (monkey.OperationNumber == -1) { monkey.OperationNumber = item; }
                switch (monkey.Operation)
                {
                    case Monkey.Operations.Multiplication:
                        inspectedItem = item * monkey.OperationNumber;
                        break;
                    case Monkey.Operations.Plus:
                        inspectedItem = item + monkey.OperationNumber;
                        break;
                    case Monkey.Operations.Minus:
                        inspectedItem = item + monkey.OperationNumber;
                        break;
                }
                if (monkey.OperationNumber == item)
                {
                    monkey.OperationNumber = -1;
                }
                var test = (inspectedItem / 3) % monkey.TestDivisor == 0;
                var monkeyToThrowTo = FindByName(monkeys, test ? monkey.CheckTrueThrowTo.ToString() : monkey.CheckFalseThrowTo.ToString());
                if (monkeyToThrowTo != null)
                {
                    monkeyToThrowTo.Items.Enqueue(inspectedItem / 3);
                }
                else
                {
                    throw new Exception("Not found");
                }
            }
        }
    }

    private static Monkey? FindByName(List<Monkey> monkeys, string name)
    {
        return monkeys.Find(x => x.Name == name.ToString());
    }

    public static List<Monkey> Parse(this string[] lines)
    {
        var monkeys = new List<Monkey>();
        foreach (var line in lines)
        {
            if (line.StartsWith("Monkey"))
            {
                monkeys.Add(new Monkey { Name = line.Split(" ").Last().Replace(":", "") });
            }
            else if (line.Contains("Starting items"))
            {
                monkeys.Last().Items = new Queue<int>(line.Split(":").Last().Trim().Split(',').Select(x => int.Parse(x.Trim())));
            }
            else if (line.Contains("Operation"))
            {
                var operation = line.Split(':').Last().Trim();
                var sign = operation.Split('=').Last().Trim().Split(" ")[1].Trim();
                var operationValue = operation.Split('=').Last().Trim().Split(" ")[2].Trim();
                monkeys.Last().OperationNumber = operationValue != "old" ? int.Parse(operationValue) : -1;
                switch (sign)
                {
                    case "*":
                        monkeys.Last().Operation = Monkey.Operations.Multiplication;
                        break;
                    case "-":
                        monkeys.Last().Operation = Monkey.Operations.Minus;
                        break;
                    case "+":
                        monkeys.Last().Operation = Monkey.Operations.Plus;
                        break;
                    case "/":
                        monkeys.Last().Operation = Monkey.Operations.Division;
                        break;
                }
            }
            else if (line.Contains("Test"))
            {
                monkeys.Last().TestDivisor = int.Parse(line.Split(" ").Last().Trim());
            }
            else if (line.Contains("If true"))
            {
                monkeys.Last().CheckTrueThrowTo = int.Parse(line.Split(" ").Last().Trim());
            }
            else if (line.Contains("If false"))
            {
                monkeys.Last().CheckFalseThrowTo = int.Parse(line.Split(" ").Last().Trim());
            }
        }
        return monkeys;
    }
}

public class Monkey
{
    public enum Operations
    {
        Plus,
        Minus,
        Division,
        Multiplication
    }
    public string Name { get; set; }
    public int InspectionCount { get; set; }
    public Operations Operation { get; set; }
    public Queue<int> Items { get; set; }
    public int TestDivisor { get; set; }
    public int CheckFalseThrowTo { get; set; }
    public int CheckTrueThrowTo { get; set; }
    public int OperationNumber { get; set; }
}