using System.Linq;
using System.IO;
using System;

Console.WriteLine(File.ReadAllLines("input.txt").Select(x => x.ExtractItemTypePriority().Priority).Sum());

public static class RucksackParser
{
    const int ASCII_UPPER_A = 65;
    const int ASCII_LOWER_A = 97;
    const int LOWER_ITEM_OFFSET = 1;
    const int UPPER_ITEM_OFFSET = 27;

    public static RucksackItem ExtractItemTypePriority(this string rucksack)
    {
        var firstPart = rucksack.Substring(0, rucksack.Length / 2);
        var secondPart = rucksack.Substring(rucksack.Length / 2);
        var itemtype = firstPart.ToCharArray().Distinct().Intersect(secondPart.ToCharArray().Distinct()).First().ToString();
        var @char = itemtype.ToCharArray().First();
        var priority = char.IsLower(@char) ? (@char - ASCII_LOWER_A + LOWER_ITEM_OFFSET) : @char - ASCII_UPPER_A + UPPER_ITEM_OFFSET;
        return new RucksackItem(@char, priority);
    }
}

public record RucksackItem(char PriorityType, int Priority);
