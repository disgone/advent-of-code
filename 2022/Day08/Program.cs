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

int bestScore = 0;
for (int x = 0; x < rows; x++)
{
    for (int y = 0; y < columns; y++)
    {
        if (x == 0 || x == rows - 1 || y == 0 || y == columns - 1)
        {
            continue;
        }

        var currentTree = grid[y,x];
        var scenicScore = 1;
        int cx, cy;

        for (cx = x - 1; cx > 0; --cx)
        {
            if (grid[y,cx].Height >= currentTree.Height)
            {
                break;
            }
        }
        scenicScore *= x - cx;

        for (cy = y - 1; cy > 0; --cy)
        {
            if (grid[cy,x].Height >= currentTree.Height)
            {
                break;
            }
        }
        scenicScore *= y - cy;

        for (cx = x + 1; cx < data.Length - 1; ++cx)
        {
            if (grid[y,cx].Height >= currentTree.Height)
            {
                break;
            }
        }
        scenicScore *= cx - x;

        for (cy = y + 1; cy < data[0].Length - 1; ++cy)
        {
            if (grid[cy,x].Height >= currentTree.Height)
            {
                break;
            }
        }
        scenicScore *= cy - y;

        if (scenicScore > bestScore)
        {
            bestScore = scenicScore;
        }
    }
}

Console.WriteLine($"The best scenic score is {bestScore}");



record Tree(int Height, bool IsVisible = false)
{
    public bool IsVisible { get; set; } = IsVisible;
}