Queue<string> instructions = new();
foreach (var line in File.ReadAllLines("input.txt"))
{
    instructions.Enqueue(line);
}

bool[,] screen = new bool[6, 40];

long registerValue = 1;
long cycle = 1;
int row = 0;
int pixel = 0;
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
        long leftBound = registerValue - 1;
        long rightBound = registerValue + 1;

        if (pixel >= leftBound && pixel <= rightBound)
        {
            screen[row, pixel] = true;
        }
        else
        {
            screen[row, pixel] = false;
        }
        
        pixel++;

        if (cycle % 40 == 0)
        {
            row++;
            pixel = 0;
        }

        cycle++;
    }

    registerValue += delta;
}

for (int r = 0; r <= screen.GetUpperBound(0); r++)
{
    for (int c = 0; c <= screen.GetUpperBound(1); c++)
    {
        if (screen[r, c])
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write('#');
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write('.');
        }
    }
    Console.Write(Environment.NewLine);
}
