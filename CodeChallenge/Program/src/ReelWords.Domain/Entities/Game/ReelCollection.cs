using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ReelWords.Domain.Entities.Game;

public class ReelCollection
{
    public const int DefaultReelsAmount = 6;
    private const int NotFoundReelIndex = -1;
    private readonly Reel[] _reels;

    private ReelCollection() { }
    private ReelCollection(IEnumerable<string> reelValues, int? amount = null) 
    {
        Amount = amount ?? DefaultReelsAmount;

        if (reelValues is null) throw new ArgumentException("Reel values shouldn't be null");
        _reels = reelValues.Select(reelValue => Reel.CreateReel(reelValue)).ToArray();
        if (_reels.Length != Amount) throw new ArgumentException($"Amount of reels should be {Amount}");
    }

    public static ReelCollection CreateReelCollection(IEnumerable<string> reelValues, int? amount = null)
    {
        return new ReelCollection(reelValues, amount);
    }

    public int Amount { get; }

    public IEnumerable<char> GetValidCharacters() => _reels.Select(reel => reel.LookupNext());

    public bool ValidateWord(string word) => ValidateWord(GetWordReels(word), word);

    public bool PlayWord(string word)
    {
        var wordReels = GetWordReels(word);

        if (!ValidateWord(wordReels, word)) return false;

        foreach (var reel in wordReels)
        {
            reel.MoveNext();
        }

        return true;
    }

    public void Shuffle()
    {
        foreach (var reel in _reels)
        {
            reel.Shuffle();
        }
    }

    private bool ValidateWord(IList<Reel> reels, string word) => reels.Count == word.Length;

    private IList<Reel> GetWordReels(string word)
    {
        var emptyResult = new List<Reel>();
        if (word is null || word.Length > Amount) return emptyResult;
        var used = new BitArray(Amount);
        var result = new List<Reel>();
        for (int i = 0; i < word.Length; i++)
        {
            var checkCharacter = word[i];
            var foundReelIndex = FindLatestValidReelIndex(used, checkCharacter);
            if (foundReelIndex == NotFoundReelIndex) return emptyResult;
            used[foundReelIndex] = true;
            result.Add(_reels[foundReelIndex]);
        }
        return result;
    }

    private int FindLatestValidReelIndex(BitArray used, char checkCharacter)
    {
        for (int reelIndex = _reels.Length - 1; reelIndex >= 0; reelIndex--)
        {
            if (!used[reelIndex] && _reels[reelIndex].LookupNext() == checkCharacter)
            {
                return reelIndex;
            }
        }

        return NotFoundReelIndex;
    }
}
