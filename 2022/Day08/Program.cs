var data = File.ReadAllLines("input.txt");

int rows = data.Length;
int columns = data[0].Length;

Tree[,] grid = new Tree[rows, columns];

int maxHeightY = 0;
for (int x = 0; x < rows; x++)
{
    var line = data[x];
    int maxHeightX = 0;

    for (int y = 0; y < columns; y++)
    {
        int height = int.Parse(line[y].ToString());

        // The edges of our matrix are visible by default.
        bool isVisible = x == 0 || y == 0 || x == columns - 1 || y == rows - 1;

        if (height > maxHeightX)
        {
            isVisible = true;
            maxHeightX = height;
        }

        if (height > maxHeightY)
        {
            isVisible = true;
            maxHeightY = height;
        }

        grid[x, y] = new Tree(height, isVisible);
    }
}


// Set visibility from right-left, bottom-top
maxHeightY = 0;
for (int x = grid.GetUpperBound(1); x >= 0; x--)
{
    int maxHeightX = 0;
    for (int y = grid.GetUpperBound(0); y >= 0; y--)
    {
        var tree = grid[x, y];

        if (tree.Height > maxHeightX)
        {
            tree.IsVisible = true;
            maxHeightX = tree.Height;
        }

        if (tree.Height > maxHeightY)
        {
            tree.IsVisible = true;
            maxHeightX = tree.Height;
        }
    }
}

for (int x = 0; x <= grid.GetUpperBound(1); x++)
{
    for (int y = 0; y <= grid.GetUpperBound(0); y++)
    {
        Console.Write($"{(grid[x,y].IsVisible ? 1 : 0)} ");
    }
    Console.Write(Environment.NewLine);
}

// for (int x = 0; x <= grid.GetUpperBound(1); x++)
// {
//     for (int y = 0; y <= grid.GetUpperBound(0); y++)
//     {
//         Console.Write($"{grid[x,y].Height} ");
//     }
//     Console.Write(Environment.NewLine);
// }

int totalVisible = 0;
for (int x = 0; x <= grid.GetUpperBound(1); x++)
{
    for (int y = 0; y <= grid.GetUpperBound(0); y++)
    {
        totalVisible += grid[x, y].IsVisible ? 1 : 0;
    }
}

Console.WriteLine($"{totalVisible} trees are visible");

record Tree(int Height, bool IsVisible = false)
{
    public bool IsVisible { get; set; } = IsVisible;
}