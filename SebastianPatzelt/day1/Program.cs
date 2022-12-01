using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

Console.WriteLine(
    File.ReadLines("input.txt")
        .Aggregate(new List<List<int>> { new List<int>() },
        (list, value) =>
        {
            if (int.TryParse(value, out var number)) list.Last().Add(number);
            else list.Add(new List<int>());
            return list;
        })
        .Select(x => x.Sum())
        .Max(x => x));