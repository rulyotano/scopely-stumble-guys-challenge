using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReelWords.Domain.Entities.Game;

public interface IGameManager
{
    PlayWordResult PlayWord(string playerId, string word);
    IEnumerable<char> GetValidCharacters(string playerId);
    int GetPoints(string playerId);
}

public class GameManager: IGameManager
{
    private readonly IDictionary<string, PlayerData> _players;
    private readonly IReelsProvider _reelsProvider;
    private ValidWords _validWords;
    private Scores _scores;

    private GameManager(IReelsProvider reelsProvider)
    {
        _players = new Dictionary<string, PlayerData>();
        _reelsProvider = reelsProvider;
    }

    public static async Task<GameManager> CreateGameManager(IReelsProvider reelsProvider, IScoresProvider scoresProvider, IValidWordsProvider validWordsProvider)
    {
        var gameManager = new GameManager(reelsProvider);
        gameManager._scores = await scoresProvider.GetScores();
        gameManager._validWords = await validWordsProvider.GetValidWords();
        return gameManager;
    }

    public async Task InitializeGame(IEnumerable<string> playerIds)
    {
        foreach (var playerId in playerIds)
        {
            await RegisterPlayerAsync(playerId);
        }
    }

    public PlayWordResult PlayWord(string playerId, string word)
    {
        var playerData = _players[playerId];
        var playerReel = playerData.Reels;
        var result = ValidateWord(playerReel, word);

        if (result == PlayWordResult.Success)
        {
            var wordScore = _scores.GetWordScore(word);
            playerReel.PlayWord(word);
            playerData.Score.AddScore(word, wordScore);
        }

        return result;
    }

    public IEnumerable<char> GetValidCharacters(string playerId)
    {
        var playerData = GetPlayeData(playerId);
        return playerData.Reels.GetValidCharacters();
    }

    public int GetPoints(string playerId)
    {
        var playerData = GetPlayeData(playerId);
        return playerData.Score.Score;

    }

    private PlayWordResult ValidateWord(ReelCollection playerReel, string word)
    {
        if (!playerReel.ValidateWord(word)) return PlayWordResult.NotWordInReel;
        if (!_validWords.IsValidWord(word)) return PlayWordResult.NotExistingWord;
        return PlayWordResult.Success;
    }

    private PlayerData GetPlayeData(string playerId)
    {
        return _players[playerId];
    }

    private async Task RegisterPlayerAsync(string playerId)
    {
        var reels = await _reelsProvider.GetReelsForPlayer(playerId);
        reels.Shuffle();
        _players.Add(playerId, new PlayerData(PlayerScore.CreatePlayerScore(), reels));
    }

    private sealed record PlayerData(PlayerScore Score, ReelCollection Reels);
}

public enum PlayWordResult
{
    Success, NotExistingWord, NotWordInReel
}
