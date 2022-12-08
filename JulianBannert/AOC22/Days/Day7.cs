using AOC22.Misc;

namespace AOC22.Days;

[Day(7)]
public class Day7 : IDailyPuzzle
{
    private const int TotalDiskSpace = 70_000_000;
    private const int RequiredFreeSpace = 30_000_000;
    private const int MaxDirectorySize = 100_000;
    
    public void Run(IEnumerable<string> inputLines)
    {
        Console.WriteLine("\nRunning Day 7:");
        var root = CreateDirectoryTree(inputLines);
        
        var part1 = FindDirectories(root,s => s < MaxDirectorySize).Sum(x => x.GetSize());
        Console.WriteLine($"Part 1: {part1}");
        
        var part2 = FindDirectories(root, s => s >= RequiredFreeSpace - (TotalDiskSpace - root.GetSize()))
            .OrderBy(x => x.GetSize())
            .First()
            .GetSize();
        Console.WriteLine($"Part 2: {part2}");
    }
    
    private static IEnumerable<Dir> FindDirectories(Dir root, Func<int, bool> addCondition)
    {
        var list = new List<Dir>();
        Recurse(root, list, addCondition);
        return list;
    }
    
    private static void Recurse(Dir current, List<Dir> list, Func<int, bool> addCondition)
    {
        if (addCondition.Invoke(current.GetSize()))
            list.Add(current);
        
        foreach (var child in current.Dirs.Values)
            Recurse(child, list, addCondition);
    }

    private static Dir CreateDirectoryTree(IEnumerable<string> inputLines)
    {
        var root = new Dir("/", null);
        Dir? currentDir = null;
        foreach (var line in inputLines)
        {
            if (line.StartsWith("$"))
            {
                var command = line[2..line.Length];
                if (!command.StartsWith("cd")) continue;
                var arg = command[3..command.Length];
                currentDir = arg switch
                {
                    ".." => currentDir!.Parent!,
                    "/" => root,
                    _ => currentDir!.Dirs[arg]
                };

            }
            else
            {
                var s = line.Split(" ");
                if (s[0] == "dir")
                    currentDir!.Dirs.Add(s[1], new Dir(s[1], currentDir));
                else
                    currentDir!.AddFile(int.Parse(s[0]));
            }
        }

        return root;
    }

    private class Dir
    {
        public string Name { get; }
        public Dir? Parent { get; }
        public readonly Dictionary<string, Dir> Dirs = new ();
        private int _fileSize;

        internal Dir(string name, Dir? parent)
        {
            Name = name;
            Parent = parent;
        }

        internal void AddFile(int size) => _fileSize += size;
        
        internal int GetSize() => _fileSize + Dirs.Sum(x => x.Value.GetSize());
    }
}