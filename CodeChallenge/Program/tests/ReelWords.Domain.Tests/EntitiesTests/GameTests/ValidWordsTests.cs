using ReelWords.Domain.Entities.Game;
using System;

namespace ReelWords.Domain.Tests.EntitiesTests.GameTests;

public class ValidWordsTests
{
    [Fact]
    public void WhenNullWords_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => ValidWords.CreateValidWords(null));
    }

    [Fact]
    public void ShouldValidateWordsCorrectly()
    {
        var validWords = ValidWords.CreateValidWords(new string[] { "boat", "tree", "bowl", "threat", "abcs", "bee", "animal", "animate" });
        Assert.True(validWords.IsValidWord("boat"));
        Assert.True(validWords.IsValidWord("tree"));
        Assert.True(validWords.IsValidWord("bowl"));
        Assert.True(validWords.IsValidWord("abcs"));
        Assert.True(validWords.IsValidWord("bee"));
        Assert.True(validWords.IsValidWord("animate"));
        Assert.True(validWords.IsValidWord("animal"));

        Assert.False(validWords.IsValidWord("car"));
        Assert.False(validWords.IsValidWord("beat"));
        Assert.False(validWords.IsValidWord("air"));
        Assert.False(validWords.IsValidWord("beat"));
        Assert.False(validWords.IsValidWord("abc"));
    }

    [Fact]
    public void When_StrangeCharactersShouldCovertToRawEnglish()
    {
        var validWords = ValidWords.CreateValidWords(new string[] { "Bóa´ª't", "caña", "g o a l" });
        Assert.True(validWords.IsValidWord("boat"));
        Assert.True(validWords.IsValidWord("cana"));
        Assert.True(validWords.IsValidWord("goal"));
    }

    [Fact]
    public void WhenNullWord_ShouldIngonre()
    {
        var validWords = ValidWords.CreateValidWords(new string[] { "boat", "tree", null, "bowl" });
        Assert.True(validWords.IsValidWord("bowl"));
    }
}
