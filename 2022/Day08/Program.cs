var data = File.ReadAllLines("input.txt");

int rows = data.Length;
int columns = data[0].Length;

Tree[,] grid = new Tree[rows, columns];

int[] maxHeightY = new int[columns];
for (int y = 0; y < rows; y++)
{
    var line = data[y];
    int maxHeightX = 0;
    for (int x = 0; x < columns; x++)
    {
        int height = int.Parse(line[x].ToString());

        // The edges of our matrix are visible by default.
        bool isVisible = y == 0 || y == columns - 1 || x == 0  || x == rows - 1;

        if (maxHeightY[x] < height)
        {
            maxHeightY[x] = height;
            isVisible = true;
        }

        if (maxHeightX < height)
        {
            maxHeightX = height;
            isVisible = true;
        }

        grid[y, x] = new Tree(height, isVisible);
    }
}

maxHeightY = new int[columns];
for (int y = rows - 1; y >= 0; y--)
{
    int maxHeightX = 0;
    for (int x = columns - 1; x >= 0; x--)
    {
        var tree = grid[y, x];

        if (maxHeightY[x] < tree.Height)
        {
            maxHeightY[x] = tree.Height;
            tree.IsVisible = true;
        }

        if (maxHeightX < tree.Height)
        {
            maxHeightX = tree.Height;
            tree.IsVisible = true;
        }
    }
}

int totalVisible = 0;
for (int y = 0; y <= grid.GetUpperBound(1); y++)
{
    for (int x = 0; x <= grid.GetUpperBound(0); x++)
    {
        totalVisible += grid[y, x].IsVisible ? 1 : 0;
    }
}
Console.WriteLine($"{totalVisible} trees are visible");

record Tree(int Height, bool IsVisible = false)
{
    public bool IsVisible { get; set; } = IsVisible;
}