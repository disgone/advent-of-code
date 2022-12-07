int completeOverlaps = 0;
foreach (string line in File.ReadLines("input.txt"))
{
    var elfAreas = line.Split(',');
    var a = GetAssignments(elfAreas[0]);
    var b = GetAssignments(elfAreas[1]);

    // A encapsulates B, or B encapsulates A
    if ((a.start <= b.start && a.end >= b.end) || (b.start <= a.start && b.end >= a.end))
    {
        completeOverlaps++;
    }
}

static (int start, int end) GetAssignments(string range)
{
    var assignments = range.Split('-').Select(int.Parse).ToArray();
    return (assignments[0], assignments[1]);
}

Console.WriteLine($"There are {completeOverlaps} groups with complete overlaps");