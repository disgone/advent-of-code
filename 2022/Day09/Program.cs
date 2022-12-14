var moves = from line in File.ReadAllLines("input.txt")
            let parts = line.Split(' ')
            select new Move(parts[0], int.Parse(parts[1]));

var start = new Knot();
var end = new Knot();

foreach (var move in moves)
{
    for (int i = 1; i <= move.Distance; i++)
    {
        start.Move(move.Direction);
        end.Follow(start);
    }
}

Console.WriteLine($"The tail visited {end.Locations.Count()} unique locations");

public record Move(string Direction, int Distance);

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