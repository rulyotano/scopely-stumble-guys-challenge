using ReelWords.CrossCutting.Helpers;
using System;

namespace ReelWords.CrossCutting.Tests.HelpersTests.StringHelpersTests;

public class StringSanitizeTests
{
    [Theory]
    [InlineData("Prueba")]
    [InlineData("ABC")]
    [InlineData("abc")]
    public void Should_ConvertToLowerCase(string testValue)
    {
        Assert.Equal(testValue.ToLowerInvariant(), testValue.Sanitize());
    }

    [Theory]
    [InlineData("Pruebas^")]
    [InlineData("P rue bas")]
    [InlineData("P.ruebas")]
    [InlineData("Prueba's")]
    [InlineData("$Pruebas")]
    [InlineData("P#ruebas")]
    [InlineData("Pru*eb$as")]
    public void Should_OmitOtherCharactersThanEnglishLetters(string testValue)
    {
        Assert.Equal("pruebas", testValue.Sanitize());
    }

    [Theory]
    [InlineData("España", "espana")]
    [InlineData("paragüas", "paraguas")]
    [InlineData("áéíóú", "aeiou")]
    [InlineData("àèìòù", "aeiou")]
    [InlineData("âêîôû", "aeiou")]
    [InlineData("äëïöü", "aeiou")]
    [InlineData("åů", "au")]
    [InlineData("Araçatuba", "aracatuba")]
    [InlineData("Ångström", "angstrom")]
    [InlineData("Übermensch's", "ubermenschs")]
    public void Should_ConvertSimilarLettersToEnglishVariant(string testValue, string expected)
    {
        Assert.Equal(expected, testValue.Sanitize());
    }

    [Fact]
    public void WhenNull_ShouldReturnArgumentException()
    {
        Assert.Throws<ArgumentException>(() => StringHelpers.Sanitize(null));
    }
}
