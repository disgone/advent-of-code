Queue<string> instructions = new();
foreach (var line in File.ReadAllLines("input.txt"))
{
    instructions.Enqueue(line);
}

long registerValue = 1;
long cycle = 1;
List<long> checkPoints = new();
while (instructions.TryDequeue(out string current))
{
    int burn = 1;
    int delta = 0;
    if (current != "noop")
    {
        burn = 2;
        
        delta = int.Parse(current.Split(' ')[1]);
    }

    for (int i = 1; i <= burn; i++)
    {
        if (cycle == 20 || (cycle - 20) % 40 == 0)
        {
            checkPoints.Add(registerValue * cycle);
        }
        cycle++;
    }

    registerValue += delta;
}

Console.WriteLine(String.Join(',', checkPoints));
Console.WriteLine(checkPoints.Sum());