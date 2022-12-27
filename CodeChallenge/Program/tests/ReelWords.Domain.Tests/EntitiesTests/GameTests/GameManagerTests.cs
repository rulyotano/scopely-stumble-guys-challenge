using Moq;
using ReelWords.Domain.Entities.Game;
using System;
using System.Threading.Tasks;

namespace ReelWords.Domain.Tests.EntitiesTests.GameTests;

public class GameManagerTests
{
    private readonly Mock<IReelsProvider> _reelsProviderMock;
    private readonly Mock<IScoresProvider> _scoresProviderMock;
    private readonly Mock<IValidWordsProvider> _validWordsProviderMock;
    private GameManager _gameManager;
    private ReelCollection _reels;
    private ValidWords _validWords;
    private Scores _scores;
    private const string PlayerId = "fake-player-id";
    private const string NotPlayerId = "not-initialized-player-id";
    private const string Valid1 = "valid";
    private const string Valid2 = "boats";
    private const string NotValid = "lidva";

    public GameManagerTests()
    {
        _reelsProviderMock = new Mock<IReelsProvider>();
        _scoresProviderMock = new Mock<IScoresProvider>();
        _validWordsProviderMock = new Mock<IValidWordsProvider>();

        SetupReels();
        SetupValidWords();
        SetupScores();
        SetupGameManager().Wait();
    }

    [Fact]
    public void WhenInitializeGameAndGetPoints_ShouldInitiallyReturnZero()
    {
        Assert.Equal(0, _gameManager.GetPoints(PlayerId));
    }

    [Fact]
    public void WhenGetPointsAndNoPlayerId_ShouldInitiallyReturnZero()
    {
        Assert.Throws<ArgumentException>(() => _gameManager.GetPoints(NotPlayerId));
    }

    [Fact]
    public void WhenValidWord_ShouldIncresePoints()
    {
        var result = _gameManager.PlayWord(PlayerId, Valid1);
        Assert.Equal(PlayWordResult.Success, result);
        Assert.Equal(_scores.GetWordScore(Valid1), _gameManager.GetPoints(PlayerId));
    }

    [Fact]
    public void WhenWordNotInReel_ShouldReturnNotWordInReel()
    {
        var result = _gameManager.PlayWord(PlayerId, Valid2);
        Assert.Equal(PlayWordResult.NotWordInReel, result);
        Assert.Equal(0, _gameManager.GetPoints(PlayerId));
    }

    [Fact]
    public void WhenWordNotInDictionary_ShouldReturnNotExistingWord()
    {
        var result = _gameManager.PlayWord(PlayerId, NotValid);
        Assert.Equal(PlayWordResult.NotExistingWord, result);
        Assert.Equal(0, _gameManager.GetPoints(PlayerId));
    }

    [Fact]
    public void WhenValidWord_ShouldMoveReels()
    {
        _gameManager.PlayWord(PlayerId, Valid1);
        var result = _gameManager.PlayWord(PlayerId, Valid2);
        Assert.Equal(PlayWordResult.Success, result);
        Assert.Equal(_scores.GetWordScore(Valid1) + _scores.GetWordScore(Valid2), _gameManager.GetPoints(PlayerId));
    }

    [Fact]
    public void WhenGettingValidCharacters_ShouldActuallyReturnThem()
    {
        var word = GetWordFromReels();
        var result = _gameManager.PlayWord(PlayerId, word);
        Assert.Equal(PlayWordResult.Success, result);
        Assert.Equal(_scores.GetWordScore(word), _gameManager.GetPoints(PlayerId));
    }

    [Fact]
    public void WhenValidWordButNotInLowercase_ShouldAlsoAcceptIt()
    {
        var result = _gameManager.PlayWord(PlayerId, Valid1.ToUpper());
        Assert.Equal(PlayWordResult.Success, result);
    }

    [Fact]
    public void WhenNullWord_ShouldReturnNotExistingWord()
    {
        var result = _gameManager.PlayWord(PlayerId, null);
        Assert.Equal(PlayWordResult.NotExistingWord, result);
    }

    private void SetupReels()
    {
        _reels = ReelCollection.CreateReelCollection(new string[] { "vb", "ao", "la", "it", "ds" }, 5);
        _reelsProviderMock.Setup(it => it.GetReelsForPlayer(PlayerId)).ReturnsAsync(_reels);
    }

    private async Task SetupGameManager()
    {
        _gameManager = await GameManager.CreateGameManager(_reelsProviderMock.Object, _scoresProviderMock.Object, _validWordsProviderMock.Object);
        await _gameManager.InitializeGame(new string[] { PlayerId }, false);
    }

    private void SetupValidWords()
    {
        _validWords = ValidWords.CreateValidWords(new string[] { Valid1, Valid2 });
        _validWordsProviderMock.Setup(it => it.GetValidWords()).ReturnsAsync(_validWords);
    }

    private void SetupScores()
    {
        _scores = Scores.CreateScores(new (char, int)[] { ('v', 2), ('a', 1), ('l', 2), ('i', 1), ('d', 2), ('b', 3), ('o', 1), ('t', 3), ('s', 2) });
        _scoresProviderMock.Setup(it => it.GetScores()).ReturnsAsync(_scores);
    }

    private string GetWordFromReels()
    {
        var characters = _reels.GetValidCharacters();
        return string.Join("", characters);
    }
}
