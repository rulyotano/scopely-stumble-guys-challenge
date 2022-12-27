using System;
using System.Collections.Generic;
using System.Linq;

namespace ReelWords.Domain.Entities.Game;

public class Scores
{
    private readonly IDictionary<char, int> _scores;

    private Scores() { }
    private Scores(IEnumerable<(char character, int score)> scores) 
    {
        if (scores is null) throw new ArgumentException("Score collection shouldn't be null");
        _scores = scores.ToDictionary(it => it.character, it => it.score);
    }

    public static Scores CreateScores(IEnumerable<(char character, int score)> scores)
    {
        return new Scores(scores);
    }

    public int GetWordScore(string word)
    {
        if (string.IsNullOrWhiteSpace(word)) return 0;
        return word.Sum(character => _scores.ContainsKey(character) ? _scores[character] : 0);
    }
}
