using ReelWords.CrossCutting.DataStructures;
using ReelWords.CrossCutting.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReelWords.Domain.Entities.Game;

public class ValidWords
{
    private readonly Trie _validWords;

    private ValidWords()
    {
        _validWords = new Trie();
    }

    private ValidWords(IEnumerable<string> words) : this()
    {
        if (words is null) throw new ArgumentException("Words shouldn't be null");
        FillValidWords(_validWords, words);
    }

    public static ValidWords CreateValidWords(IEnumerable<string> validWords)
    { return new ValidWords(validWords); }

    private static void FillValidWords(Trie validWords, IEnumerable<string> words)
    {
        foreach (var word in words.Where(word => word is not null).Select(word => word.Sanitize()))
        {
            validWords.Insert(word);
        }
    }

    public bool IsValidWord(string word)
    {
        return _validWords.Search(word);
    }
}
