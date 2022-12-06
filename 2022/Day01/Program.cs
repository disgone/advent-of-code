long currentElf = 0L;
long maxCalories = 0L;
await foreach (string line in File.ReadLinesAsync("input.txt"))
{
    if (long.TryParse(line, out long calories))
    {
        currentElf += calories;
    }
    else
    {
        if (currentElf > maxCalories)
        {
            maxCalories = currentElf;
        }

        currentElf = 0;
    }
}

Console.WriteLine($"Max calories held is {maxCalories}");