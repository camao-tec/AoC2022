using System.Text.RegularExpressions;

record DirectoryEntry(string Name, string fullPath, DirectoryEntry? Parent, IDictionary<string, DirectoryEntry?> SubDirectories, IEnumerable<FileEntry> Files);
record FileEntry(string Name, int Size);

internal class Program
{
    private static readonly Regex cdRegex = new(@"\$ cd ([\/]{1}|[\.]{2}|[a-zA-Z0-9]*)");
    private static readonly Regex dirRegex = new(@"dir ([a-zA-Z0-9]*)");
    private static readonly Regex fileRegex = new(@"([0-9]+) ([a-zA-Z0-9]+.[a-z-A-Z0-9]+)");

    const int FILESYSTEM_SIZE = 70000000;
    const int SPACE_REQUIRED = 30000000;
    const int MAX_SIZE = 100000;


    private static async Task Main(string[] args)
    {
        var input = await File.ReadAllTextAsync("input.txt");

        var commandBlocks = cdRegex.Matches(input)
            .TakeWhile(m => m.Success)
            .Select(m => input[m.Index..(m.NextMatch().Success ? m.NextMatch().Index : input.Length)])
            .ToList();

        var structure = BuildDirectoryStructure(commandBlocks);

        // part 1
        var dirSizeMap = new Dictionary<string, int>();
        var totalSize = GetTotalSizePartOne(structure, dirSizeMap);
        Console.WriteLine($"part 1: {dirSizeMap.Values.Where(size => size <= MAX_SIZE).Sum()}");

        // part 2
        var targetDirSize = SPACE_REQUIRED - (FILESYSTEM_SIZE - totalSize);
        var dirSizeToDelete = dirSizeMap
            .Where(d => d.Value >= targetDirSize)
            .OrderBy(d => d.Value)
            .FirstOrDefault();
        Console.WriteLine($"part 2: {dirSizeToDelete.Key} with size of {dirSizeToDelete.Value}");
    }

    private static int GetTotalSizePartOne(DirectoryEntry? entry, IDictionary<string, int> dirSizeMap)
    {
        if (entry == null)
        {
            return 0;
        }

        var fileSizes = entry.Files.Any()
           ? entry.Files.Select(f => f.Size).Aggregate((a, b) => a + b)
           : 0;

        var dirSize = fileSizes + entry.SubDirectories.Select(d => GetTotalSizePartOne(d.Value, dirSizeMap)).Sum();
        dirSizeMap.Add(entry.fullPath, dirSize);

        return dirSize;
    }

    private static DirectoryEntry? BuildDirectoryStructure(
        IList<string> commandBlocks,
        int index = 0,
        DirectoryEntry? parent = null)
    {
        var commandBlock = commandBlocks.ElementAt(index);
        if (string.IsNullOrWhiteSpace(commandBlock))
        {
            throw new ArgumentException("invalid command");
        }

        var changeDirCommand = cdRegex.Match(commandBlock);
        if (!changeDirCommand.Success || changeDirCommand.Groups.Count <= 1)
        {
            throw new ArgumentException("invalid command");
        }

        var dirName = changeDirCommand.Groups[1].Value;
        if (dirName.StartsWith("..") && parent != null)
        {
            return BuildDirectoryStructure(commandBlocks, index + 1, parent.Parent);
        }

        var entry = new DirectoryEntry(
            Name: dirName,
            fullPath: string.IsNullOrEmpty(parent?.fullPath) ? dirName : $"{parent.fullPath}{dirName}/",
            Parent: parent,
            SubDirectories: dirRegex.Matches(commandBlock)
                .Where(m => m.Success && m.Groups.Count > 1)
                .ToDictionary(m => m.Groups[1].Value, _ => null as DirectoryEntry),
            Files: fileRegex.Matches(commandBlock)
                .Where(m => m.Success && m.Groups.Count > 2)
                .Select(m => new FileEntry(
                    m.Groups[2].Value,
                    int.TryParse(m.Groups[1].Value, out var size) ? size : 0))
                .ToList());

        if (parent != null && parent.SubDirectories.ContainsKey(dirName))
        {
            parent.SubDirectories[dirName] = entry;
        }

        if (index + 1 < commandBlocks.Count)
        {
            BuildDirectoryStructure(commandBlocks, index + 1, entry);
        }

        return entry;
    }
}