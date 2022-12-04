using System.Linq;
using System.IO;
using System;
using System.Collections.Generic;

Console.WriteLine(File.ReadAllLines("input.txt").Select(x => x.ToRanges().HasFullSectionInCommon()).Count(x => x));
Console.WriteLine(File.ReadAllLines("input.txt").Select(x => x.ToRanges().HasAnySectionInCommon()).Count(x => x));

public static class PairParser
{
    public static bool HasFullSectionInCommon(this IEnumerable<int>[] ranges)
    {
        var intersection = ranges[0].Intersect(ranges[1]);
        return intersection.Count() == ranges[0].Count() || intersection.Count() == ranges[1].Count();
    }
    public static bool HasAnySectionInCommon(this IEnumerable<int>[] ranges)
    {
        return ranges[0].Intersect(ranges[1]).Any();
    }
    public static IEnumerable<int>[] ToRanges(this string ranges)
    {
        var first = ranges.Split(',').First();
        var second = ranges.Split(',').Last();
        return new[] { first.ToRange(), second.ToRange() };
    }
    public static IEnumerable<int> ToRange(this string range)
    {
        var parts = range.Split('-');
        var start = int.Parse(parts.First());
        var end = int.Parse(parts.Last());
        return Enumerable.Range(start, end - start + 1);
    }
}