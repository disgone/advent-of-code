var elfCalories = new List<long>();

long currentElf = 0L;

await foreach (string line in File.ReadLinesAsync("input.txt"))
{
    if (long.TryParse(line, out long calories))
    {
        currentElf += calories;
    }
    else
    {
        elfCalories.Add(currentElf);
        currentElf = 0;
    }
}

var totalCalories = elfCalories.OrderDescending().Take(3).Sum(n => n);

Console.WriteLine($"The top 3 elves are carrying {totalCalories} calories");