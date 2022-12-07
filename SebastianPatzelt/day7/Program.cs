using System.Linq;
using System.IO;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

var root = File.ReadAllLines("input.txt").Parse1();
const long DISKSPACE = 70000000;
const long UPDATE_SIZE = 30000000;
const long MAX_DIR_TO_DELETE_SIZE = 100000;
long updateFreeSpaceRequirement = UPDATE_SIZE - (DISKSPACE - root.Size);
var resultTuple = DirParser.ProcessDeletionCandidates(root, MAX_DIR_TO_DELETE_SIZE, updateFreeSpaceRequirement, DISKSPACE);
Console.WriteLine(resultTuple.Item1);
Console.WriteLine(resultTuple.Item2);
Console.ReadLine();

public static class DirParser
{
    public static FileSystemEntry Parse1(this string[] commands)
    {
        var current = new FileSystemEntry("/");
        var root = current;

        var listRegex = new Regex("dir (?'dir'\\w*)|((?'size'\\d*) (?'name'.*))$");
        var cdirRegex = new Regex("[$] cd (?'dir'[\\w\\/.]*)");

        foreach (var cmd in commands.Skip(1))
        {
            var cdMatch = cdirRegex.Match(cmd);
            var listMatch = listRegex.Match(cmd);
            if (cdMatch.Success)
            {
                current = current.Children[cdMatch.Groups["dir"].Value];
            }
            else if (listMatch.Success && listMatch.Groups["dir"].Success)
            {
                current.Children[listMatch.Groups["dir"].Value] = new FileSystemEntry(listMatch.Groups["dir"].Value);
                current.Children[listMatch.Groups["dir"].Value].Children[".."] = current;
            }
            else if (listMatch.Groups["size"].Success && listMatch.Groups["size"].Value != string.Empty)
            {
                current.Children[listMatch.Groups["name"].Value] = new FileSystemEntry(listMatch.Groups["name"].Value);
                current.Children[listMatch.Groups["name"].Value].Size = long.Parse(listMatch.Groups["size"].Value);
            }
        }
        SumSizes(root);
        return root;
    }

    private static long SumSizes(FileSystemEntry node)
    {
        if (node.Children.Count == 0)
        {
            return node.Size;
        }
        node.Size = node.Children.Where(x => x.Key != "..").Sum(x => SumSizes(x.Value));
        return node.Size;
    }

    public static Tuple<long, long> ProcessDeletionCandidates(FileSystemEntry dir, long maxDirSize, long spaceNeeded, long minsize, long totalSize = 0)
    {
        // Review: Das muss besser gehen
        if (dir.Children.Any())
        {
            if (dir.Size <= maxDirSize)
            {
                totalSize += dir.Size;
            }

            if (dir.Size >= spaceNeeded && dir.Size < minsize)
            {
                minsize = dir.Size;
            }
        }

        foreach (var item in dir.Children.Where(x => x.Key != ".."))
        {
            var tuple = ProcessDeletionCandidates(item.Value, maxDirSize, spaceNeeded, minsize);
            totalSize += tuple.Item1;
            minsize = Math.Min(minsize, tuple.Item2);
        }
        return new Tuple<long, long>(totalSize, minsize);
    }

    public record FileSystemEntry(string Name)
    {
        public Dictionary<string, FileSystemEntry> Children { get; set; } = new();

        public long Size { get; set; }
    }
}