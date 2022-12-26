using ReelWords.Domain.Entities.Game;
using System;
using System.Linq;

namespace ReelWords.Domain.Tests.EntitiesTests.GameTests;

public class ReelsCollectionTests
{

    [Fact]
    public void When_NullCollection_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => ReelCollection.CreateReelCollection(null));
    }

    [Fact]
    public void When_EmptyCollection_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => ReelCollection.CreateReelCollection(Enumerable.Empty<string>()));
    }

    [Fact]
    public void When_DifferentAmountOfExpectedReels_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => ReelCollection.CreateReelCollection(new string[] { "abc", "efg" }));
    }

    [Fact]
    public void When_ExpectedAmountOfReels_ShouldCreateCorrectly()
    {
        ReelCollection.CreateReelCollection(new string[] { "a", "b", "c", "d", "e", "f" });
        Assert.True(true);
    }

    [Fact]
    public void When_CustomAmountOfReels_ShouldCreateCorrectly()
    {
        ReelCollection.CreateReelCollection(new string[] { "a", "b" }, 2);
        Assert.True(true);
    }

    [Fact]
    public void ShouldReturnNextToPlay()
    {
        var reelCollection = ReelCollection.CreateReelCollection(new string[] { "abc", "def" }, 2);
        var result = reelCollection.GetValidCharacters();
        Assert.Collection(result, c1 => Assert.Equal('a', c1), c2 => Assert.Equal('d', c2));
        
    }

    [Theory]
    [InlineData("rake", true)]
    [InlineData("rakerake", false)]
    [InlineData("dkraek", true)]
    [InlineData("dkaeke", false)]
    [InlineData("a", true)]
    [InlineData("date", false)]
    [InlineData("kk", true)]
    public void ShouldValidateCorrectly(string word, bool expectedResult)
    {
        var reelCollection = ReelCollection.CreateReelCollection(new string[] { "da", "kr", "ru", "ab", "ef", "kl" });
        var result = reelCollection.ValidateWord(word);
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void When_PlayWord_ShouldMoveTheReels()
    {
        var reelCollection = ReelCollection.CreateReelCollection(new string[] { "da", "kr", "ru", "ab", "ef", "kl" });
        const string firstWord = "dreak";
        const string nexWord = "fkalub";
        Assert.True(reelCollection.ValidateWord(firstWord));
        Assert.False(reelCollection.ValidateWord(nexWord));

        reelCollection.PlayWord(firstWord);
        Assert.False(reelCollection.ValidateWord(firstWord));
        Assert.True(reelCollection.ValidateWord(nexWord));
    }

    [Fact]
    public void When_Shuffle_ShouldChangeTheReels()
    {
        var reelCollection = ReelCollection.CreateReelCollection(new string[] { "abcdefuvwxyz", "ghijklmnopqrst" }, 2);
        Assert.True(reelCollection.ValidateWord("ga"));

        reelCollection.Shuffle();

        Assert.False(reelCollection.ValidateWord("ga"));
        Assert.True(reelCollection.ValidateWord(string.Join(string.Empty, reelCollection.GetValidCharacters())));
    }
}
