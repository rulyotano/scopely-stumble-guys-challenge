using System;
using System.Collections.Generic;
using System.Linq;

namespace ReelWords.Domain.Tests.EntitiesTests.GameTests;

public class ReelTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    public void When_CreatingWithEmptyReelValue_ShouldThrowArgumentException(string emptyValue)
    {
        Assert.Throws<ArgumentException>(() => Entities.Game.Reel.CreateReel(emptyValue));
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("cbd")]
    [InlineData("erd")]
    [InlineData("dfhwrwresdg")]
    public void When_Creating_ShouldStartBeTheBeiningByDefault(string reelValue)
    {
        var reel = Entities.Game.Reel.CreateReel(reelValue);
        Assert.Equal(reelValue[0], reel.LookupNext());        
    }

    [Fact]
    public void WhenMoveNext_Should_ChangeLookupToNextAndMoveOldOneToTheBegining()
    {
        var reel = Entities.Game.Reel.CreateReel("a");
        reel.MoveNext();
        Assert.Equal('a', reel.LookupNext());

        reel = Entities.Game.Reel.CreateReel("ab");
        reel.MoveNext();
        Assert.Equal('b', reel.LookupNext());
        reel.MoveNext();
        Assert.Equal('a', reel.LookupNext());

        reel = Entities.Game.Reel.CreateReel("abcd");
        reel.MoveNext();
        Assert.Equal('b', reel.LookupNext());
        reel.MoveNext();
        Assert.Equal('c', reel.LookupNext());
        reel.MoveNext();
        Assert.Equal('d', reel.LookupNext());
        reel.MoveNext();
        Assert.Equal('a', reel.LookupNext());
        reel.MoveNext();
        Assert.Equal('b', reel.LookupNext());
    }

    [Fact]
    public void WhenShuffle_Should_PickRandomLetterWithinTheAvailables()
    {
        const string reelValue = "abcde";
        var reel = Entities.Game.Reel.CreateReel(reelValue);
        var used = new HashSet<char>();

        for (int i = 0; i < 1000; i++)
        {
            reel.Shuffle();
            var current = reel.LookupNext();
            Assert.True(reelValue.Contains(current));
            used.Add(current);
        }

        Assert.True(reelValue.All(item => used.Contains(item)));
    }

    [Fact]
    public void Should_ParseReelsWithDifferentFormats()
    {
        const string reelValue = "ä b´ c d  é  ñ";
        var reel = Entities.Game.Reel.CreateReel(reelValue);

        Assert.Equal('a', reel.LookupNext());
        reel.MoveNext();
        Assert.Equal('b', reel.LookupNext());
        reel.MoveNext();
        Assert.Equal('c', reel.LookupNext());
        reel.MoveNext();
        Assert.Equal('d', reel.LookupNext());
        reel.MoveNext();
        Assert.Equal('e', reel.LookupNext());
        reel.MoveNext();
        Assert.Equal('n', reel.LookupNext());
    }
}
