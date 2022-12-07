int overlaps = 0;
foreach (string line in File.ReadLines("input.txt"))
{
    var elfAreas = line.Split(',');
    var a = GetAssignments(elfAreas[0]);
    var b = GetAssignments(elfAreas[1]);

    if (a.RoomAssignments.Intersect(b.RoomAssignments).Any())
    {
        overlaps++;
    }
}

static RoomAssignment GetAssignments(string range)
{
    var assignments = range.Split('-').Select(int.Parse).ToArray();
    return new RoomAssignment(assignments[0], assignments[1]);
}

Console.WriteLine($"There are {overlaps} groups with any overlaps");

record RoomAssignment(int Start, int End)
{
    private int[]? _roomAssignments;

    private int NumberOfRooms => End - Start + 1;

    public int[] RoomAssignments => _roomAssignments ??= Enumerable.Range(Start, NumberOfRooms).ToArray();
}