using System.Collections.Generic;
using System.Linq;

namespace ReelWords.Domain.Entities.Game;

public class PlayerScore
{
    private readonly IList<WordScore> _scores;

    private PlayerScore()
    {
        _scores = new List<WordScore>();
        Score = 0;
    }

    public int Score { get; private set; }


    public static PlayerScore CreatePlayerScore()
    {
        return new PlayerScore();
    }

    public void AddScore(string word, int score)
    {
        _scores.Add(new WordScore(word, score));
        Score += score;
    }

    public bool Audit() => _scores.Sum(it => it.Score) == Score;

    private sealed record WordScore(string Word, int Score);
}
