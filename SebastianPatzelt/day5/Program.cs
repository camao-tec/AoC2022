using System.Linq;
using System.IO;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

var stacks = new Stack<string>[9];
stacks[0] = new Stack<string>(new[] { "Q",
"H",
"C",
"T",
"N",
"S",
"V",
"B" }.Reverse());
stacks[1] = new Stack<string>(new[] { "G",
"B",
"D",
"W" }.Reverse());
stacks[2] = new Stack<string>(new[] { "B",
"Q",
"S",
"T",
"R",
"W",
"F",
}.Reverse());
stacks[3] = new Stack<string>(new[] { "N",
"D",
"J",
"Z",
"S",
"W",
"G",
"L"
}.Reverse());
stacks[4] = new Stack<string>(new[] { "F",
"V",
"D",
"P", "M" }.Reverse());
stacks[5] = new Stack<string>(new[] { "J",
"W",
"F"}.Reverse());
stacks[6] = new Stack<string>(new[] { "V",
"J",
"B",
"Q", "N", "L" }.Reverse());
stacks[7] = new Stack<string>(new[] { "N",
"S",
"Q",
"J", "C", "R", "T", "G" }.Reverse());
stacks[8] = new Stack<string>(new[] { "M",
"D",
"W",
"C", "Q", "S", "J"}.Reverse());

//File.ReadAllLines("input.txt").SkipWhile(x => !x.StartsWith("move")).ToList().ForEach(x =>
//{
//    x.ToInstruction().Move(stacks);
//});
File.ReadAllLines("input.txt").SkipWhile(x => !x.StartsWith("move")).ToList().ForEach(x =>
{
    x.ToInstruction().MoveMulti(stacks);
});
Console.WriteLine(string.Join("", stacks.Select(x => x.Peek())));

public class Instruction
{
    public int Amount { get; set; }
    public int Start { get; set; }
    public int End { get; set; }

    public void Move(Stack<string>[] stacks)
    {
        for (int i = 0; i < Amount; i++)
        {
            var item = stacks[Start - 1].Pop();
            stacks[End - 1].Push(item);
        }
    }

    public void MoveMulti(Stack<string>[] stacks)
    {
        var items = stacks[Start - 1].Take(Amount).Reverse().ToArray();
        for (int i = 0; i < Amount; i++)
        {
            stacks[Start - 1].Pop();
        }
        for (int i = 0; i < items.Count(); i++)
        {
            stacks[End - 1].Push(items[i]);
        }
    }
}

public static class Parser
{
    public static Instruction ToInstruction(this string input)
    {
        var regex = new Regex("^move (?'amount'\\d*) from (?'start'\\d*) to (?'end'\\d*)");
        var match = regex.Match(input);
        var amount = int.Parse(match.Groups["amount"].Value);
        var start = int.Parse(match.Groups["start"].Value);
        var end = int.Parse(match.Groups["end"].Value);
        return new Instruction { Amount = amount, Start = start, End = end };
    }
}