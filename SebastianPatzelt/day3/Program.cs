using System.Linq;
using System.IO;
using System;
using System.Collections.Generic;

Console.WriteLine(File.ReadAllLines("input.txt").Select(x => x.ExtractItemTypePriority().Priority).Sum());

Console.WriteLine(File.ReadLines("input.txt")
        .Aggregate(new List<List<string>> { new List<string>() },
        (list, value) =>
        {
            if (list.Last().Count < 6) list.Last().Add(value);
            else list.Add(new List<string>(new[] { value }));
            return list;
        })
        .Select(x => x.ToArray().ExtractItemTypePriority())
        .Sum());

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
        int priority = GetValueOfItemPriority(@char);
        return new RucksackItem(@char, priority);
    }

    private static int GetValueOfItemPriority(char @char)
    {
        return char.IsLower(@char) ? (@char - ASCII_LOWER_A + LOWER_ITEM_OFFSET) : @char - ASCII_UPPER_A + UPPER_ITEM_OFFSET;
    }

    public static int ExtractItemTypePriority(this string[] rucksacks)
    {
        var firstThree = rucksacks.Take(3).Select(x => x.ToCharArray().Distinct()).ToArray();
        var secondThree = rucksacks.Skip(3).Take(3).Select(x => x.ToCharArray().Distinct()).ToArray();
        var firstBadgeItemType = GetItemTypeForBadge(firstThree);
        var secondBadgeItemType = GetItemTypeForBadge(secondThree);
        return GetValueOfItemPriority(firstBadgeItemType) + GetValueOfItemPriority(secondBadgeItemType);
    }

    private static char GetItemTypeForBadge(IEnumerable<char>[] threeRucksackBadge)
    {
        return threeRucksackBadge[0].Intersect(threeRucksackBadge[1]).Intersect(threeRucksackBadge[2]).First();
    }
}

public record RucksackItem(char PriorityType, int Priority);
