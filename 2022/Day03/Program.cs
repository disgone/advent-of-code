int totalPriority = 0;

static int GetPriorityScore(char c) => c - (char.IsBetween(c, 'a', 'z') ? 96 : 38);

var group = new List<char[]>();
foreach (string line in File.ReadLines("input.txt"))
{
    if (group.Count < 3)
    {
        group.Add(line.ToCharArray());
    }

    if (group.Count == 3)
    {
        var badgeId = group[0].Intersect(group[1].Intersect(group[2])).Single();
        totalPriority += GetPriorityScore(badgeId);
        group.Clear();
    }
}

Console.WriteLine("Total priority " + totalPriority);