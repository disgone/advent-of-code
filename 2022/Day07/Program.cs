ElfDirectory root = new("/");
ElfDirectory? currentNode = root;

foreach (string line in File.ReadLines("input.txt"))
{
    if (line[..4] == "$ cd")
    {
        var subCommand = line[5..];
        
        if (subCommand == "..")
        {
            currentNode = currentNode?.Parent;
        }
        else if (subCommand == "/")
        {
            currentNode = root;
        }
        else
        {
            currentNode = currentNode?.Directories[subCommand];
        }
    }
    else if (line.StartsWith("dir"))
    {
        var name = line[4..];
        currentNode?.AddDirectory(new ElfDirectory(name, currentNode));
    }
    else if (line[..4] == "$ ls")
    {
        // ignore
    }
    else
    {
        var fileParts = line.Split(' ');
        long size = long.Parse(fileParts[0]);
        var file = new ElfFile(fileParts[1], size);
        currentNode.Files.Add(file);
    }
}

long GetFilterdSize(ElfDirectory node, long maxSize)
{
    long totalSize = 0;
    if (node.Size < maxSize)
    {
        totalSize += node.Size;
    }

    if (node.Directories.Count > 0)
    {
        foreach (var dir in node.Directories)
        {
            totalSize += GetFilterdSize(dir.Value, maxSize);
        }
    }

    return totalSize;
}

Console.WriteLine($"Total size: {GetFilterdSize(root,  100_000)}");

record ElfDirectory(string Name, ElfDirectory? Parent = null)
{
    public Dictionary<string,ElfDirectory> Directories { get; init; } = new();
    public List<ElfFile> Files { get; init; } = new();

    public long Size
    {
        get
        {
            return Files.Sum(n => n.Size) + Directories.Sum(n => n.Value.Size);
        }
    }

    public void AddDirectory(ElfDirectory directory)
    {
        Directories[directory.Name] = directory;
    }
}

record ElfFile(string Name, long Size);