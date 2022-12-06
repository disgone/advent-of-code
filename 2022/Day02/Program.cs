// [MyMove, TheirMove]
RoundResult[,] resultMatrix = {
    { RoundResult.Draw, RoundResult.Lose, RoundResult.Win },
    { RoundResult.Win, RoundResult.Draw, RoundResult.Lose },
    { RoundResult.Lose, RoundResult.Win, RoundResult.Draw }
};

static Throw DecodeMove(char moveCode) => moveCode switch
{
    'A' or 'X' => Throw.Rock,
    'B' or 'Y' => Throw.Paper,
    'C' or 'Z' => Throw.Scissor,
    _ => throw new ArgumentOutOfRangeException(nameof(moveCode), moveCode, null)
};

RoundResult JudgeResult(Throw theirMove, Throw myMove)
{
    return resultMatrix[(int)myMove, (int)theirMove];
}

int totalScore = 0;
await foreach (string line in File.ReadLinesAsync("input.txt"))
{
    if (line.Length < 3) continue;
    var theirMove = DecodeMove(line[0]);
    var myMove = DecodeMove(line[2]);

    Console.WriteLine($"{theirMove} vs {myMove}: {JudgeResult(theirMove, myMove)}");

    totalScore += ((int)myMove+1 + (int)JudgeResult(theirMove, myMove));
}

Console.WriteLine(totalScore);

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