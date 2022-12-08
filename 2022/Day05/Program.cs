using System.Text.RegularExpressions;

var lines = File.ReadAllLines("input.txt");

int movesLineIndex = 0;
var crates = new List<List<char>>();

// Read crates
do
{
    var line = lines[movesLineIndex];

    if (!line.Contains('['))
    {
        break;
    }

    int column = 0;
    var readLine = line.AsSpan();

    while (readLine.Length >= 3)
    {
        if (crates.Count <= column)
        {
            crates.Add(new List<char>());
        }

        var columnCrates = crates[column];

        if (readLine[1] != ' ')
        {
            columnCrates.Add(readLine[1]);
        }

        if (readLine.Length <= 4)
        {
            break;
        }

        readLine = readLine.Slice(4);
        column++;
    }

    movesLineIndex++;
} while(movesLineIndex < lines.Length);

movesLineIndex += 2;

Stack<char>[] crateStacks = new Stack<char>[crates.Count];
for (int i = 0; i < crates.Count; i++)
{
    var stack = new Stack<char>();
    for (int n = crates[i].Count - 1; n >= 0; n--)
    {
        stack.Push(crates[i][n]);
    }

    crateStacks[i] = stack;
}

Regex movePattern = new Regex(@"move (\d+) from (\d+) to (\d+)", RegexOptions.Compiled);
(int count, int from, int to) ParseMove(string move)
{
    var match = movePattern!.Match(move);
    return (int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value) - 1, int.Parse(match.Groups[3].Value) - 1);
}
var moves = new Queue<(int count, int from, int to)>();
while (movesLineIndex < lines.Length)
{
    if (!movePattern.IsMatch(lines[movesLineIndex]))
        break;
    
    moves.Enqueue(ParseMove(lines[movesLineIndex]));
    
    movesLineIndex++;
}

while (moves.TryDequeue(out (int count, int from, int to) move))
{
    for (int i = move.count; i > 0; i--)
    {
        crateStacks[move.to].Push(crateStacks[move.from].Pop());
    }
}

char[] toppers = new char[crateStacks.Length];
for (int i = 0; i < crateStacks.Length; i++)
{
    toppers[i] = crateStacks[i].Peek();
}

Console.WriteLine(new string(toppers));
