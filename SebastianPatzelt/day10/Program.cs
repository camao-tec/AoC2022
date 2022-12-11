using System.Linq;
using System.IO;
using System;
using System.Collections.Generic;

var memory = File.ReadAllLines("input.txt").Parse();
Console.WriteLine(memory.Where(x => new List<int> { 19, 59, 99, 139, 179, 219 }.Contains(x.Key)).Select(x => (x.Key + 1) * x.Value).Sum());
Console.WriteLine(Day10Parser.DoOutput(memory));
Console.ReadLine();

public static class Day10Parser
{
    public static string DoOutput(Dictionary<int, int> memory)
    {
        string output = "";
        const int ROW_LENGTH = 40;
        var lines = memory.Count / ROW_LENGTH;
        for (int i = 0; i < lines; i++)
        {
            for (int j = 0; j < ROW_LENGTH; j++)
            {
                var cycle = (i * ROW_LENGTH) + j + 1;
                var value = memory.Where(x => x.Key < cycle).Sum(x => x.Value);
                if (Enumerable.Range(value, 3).Contains(j + 1))
                {
                    output += '#';
                }
                else
                {
                    output += '.';
                }
            }
            output += Environment.NewLine;
        }
        return output;
    }
    public static Dictionary<int, int> Parse(this string[] lines)
    {
        var cycle = 0;
        var registerValue = 1;
        var memory = new Dictionary<int, int>();
        for (var i = 0; i < lines.Length; i++)
        {
            if (lines[i].StartsWith("noop"))
            {
                cycle++;
                memory[cycle] = registerValue;
            }
            else
            {
                var amount = int.Parse(lines[i].Split(' ').Last());
                for (var j = 0; j < 2; j++)
                {
                    cycle++;
                    if (j == 0)
                    {
                        memory[cycle] = registerValue;
                    }
                }
                registerValue += amount;
                memory[cycle] = registerValue;
            }
        }
        return memory;
    }
}