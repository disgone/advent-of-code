
static Throw DecodeMove(char moveCode) => moveCode switch
{
    'A' => Throw.Rock,
    'B' => Throw.Paper,
    'C' => Throw.Scissor,
    _ => throw new ArgumentOutOfRangeException(nameof(moveCode), moveCode, null)
};

static RoundResult DecodeResult(char resultCode) => resultCode switch
{
    'X' => RoundResult.Lose,
    'Y' => RoundResult.Draw,
    'Z' => RoundResult.Win,
    _ => throw new ArgumentOutOfRangeException(nameof(resultCode), resultCode, null)
};

Throw CounterForResult(Throw move, RoundResult result)
{
    if (result == RoundResult.Draw) return move;

    return (move, result) switch
    {
        (Throw.Paper, RoundResult.Win) => Throw.Scissor,
        (Throw.Rock, RoundResult.Win) => Throw.Paper,
        (Throw.Scissor, RoundResult.Win) => Throw.Rock,
        (Throw.Paper, RoundResult.Lose) => Throw.Rock,
        (Throw.Rock, RoundResult.Lose) => Throw.Scissor,
        (Throw.Scissor, RoundResult.Lose) => Throw.Paper
    };
}

int totalScore = 0;
await foreach (string line in File.ReadLinesAsync("input.txt"))
{
    if (line.Length < 3) continue;
    var theirMove = DecodeMove(line[0]);
    var expectedResult = DecodeResult(line[2]);

    var counter = CounterForResult(theirMove, expectedResult);

    totalScore += ((int)expectedResult) + ((int)counter + 1);
}

Console.WriteLine(totalScore + " total");

public enum Throw
{
    Rock = 0,
    Paper = 1,
    Scissor = 2
}

public enum RoundResult
{
    Win = 6,
    Lose = 0,
    Draw = 3
}