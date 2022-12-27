using ReelWords.Domain.Entities.Game;
using System;

namespace ReelWords.Domain.Tests.EntitiesTests.GameTests;

public class ScoresTests
{
    [Fact]
    public void ShouldSumAllCharactersScore()
    {
        var scores = Scores.CreateScores(new (char, int)[] { ('a', 1), ('b', 2), ('c', 2), ('d', 2) });
        Assert.Equal(7, scores.GetWordScore("badc"));
    }

    [Fact]
    public void WhenCharacterNotFound_ShouldSumZero()
    {
        var scores = Scores.CreateScores(new (char, int)[] { ('a', 1), ('b', 2), ('c', 2), ('d', 2) });
        Assert.Equal(1, scores.GetWordScore("arty"));
    }

    [Fact]
    public void WheNullScores_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => Scores.CreateScores(null));
    }
}
