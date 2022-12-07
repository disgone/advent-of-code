int totalPriorty = 0;

foreach (string line in File.ReadLines("input.txt"))
{
    var span = line.AsSpan();
    var midPoint = span.Length / 2;
    
    var a  = span.Slice(0, midPoint);
    var b = span.Slice(midPoint);

    var dupe = a.ToArray().Intersect(b.ToArray()).First();
    totalPriorty += dupe - (Char.IsBetween(dupe, 'a', 'z') ? 96 : 38);
}

Console.WriteLine("Total priority " + totalPriorty);