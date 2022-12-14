var moves = from line in File.ReadAllLines("input.txt")
            let parts = line.Split(' ')
            select new Move(parts[0], int.Parse(parts[1]));

var rope = new Rope(10);
foreach (var move in moves)
{
    rope.Move(move);
}

Console.WriteLine($"The tail visited {rope.Knots.Last().Locations.Count()} unique locations");

public record Move(string Direction, int Distance);

public class Rope
{
    public Rope(int knots)
    {
        Knots = new Knot[knots];
        for (int i = 0; i < knots; i++)
        {
            Knots[i] = new();
        }
    }
    
    public Knot[] Knots { get; }

    public void Move(Move move)
    {
        for (int i = 1; i <= move.Distance; i++)
        {
            for (int n = 0; n < Knots.Length; n++)
            {
                if (n == 0)
                {
                    Knots[n].Move(move.Direction);
                }
                else
                {
                    Knots[n].Follow(Knots[n-1]);
                }
            }
        }
    }
}

public class Knot
{
    public Knot()
    {
        X = 0;
        Y = 0;
        Track();
    }

    public IEnumerable<(int, int)> Locations => VisitedPositions.Keys;

    private int X { get; set; }
    private int Y { get; set; }
    private Dictionary<(int, int), int> VisitedPositions { get; } = new();

    public void Move(string direction)
    {
        switch (direction)
        {
            case "L":
                --X;
                break;
            case "U":
                --Y;
                break;
            case "D":
                ++Y;
                break;
            case "R":
                ++X;
                break;
        }

        Track();
    }

    public void Follow(Knot knot)
    {
        var dX = Math.Abs(knot.X - X);
        var dY = Math.Abs(knot.Y - Y);

        if (dX < 2 && dY < 2) return;

        if (knot.X == X)
        {
            Y += knot.Y > Y ? 1 : -1;
        }
        else if (knot.Y == Y)
        {
            X += knot.X > X ? 1 : -1;
        }
        else
        {
            X += knot.X > X ? 1 : -1;
            Y += knot.Y > Y ? 1 : -1;
        }

        Track();
    }
    
    private void Track() 
    {
        VisitedPositions.TryGetValue((X,Y), out int count);
        VisitedPositions[(X,Y)] = ++count;
    }
}