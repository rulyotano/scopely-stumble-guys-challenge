using ReelWords.CrossCutting.Helpers;
using System;

namespace ReelWords.Domain.Entities.Game;

public class Reel
{
    private readonly string _reel;
    private int _currentIndex;

    private Reel() { }
    private Reel(string reel)
    {
        if (string.IsNullOrWhiteSpace(reel)) 
            throw new ArgumentException("Reel value can't be empty");
            
        _reel = reel.Sanitize();
        _currentIndex = 0;
    }

    public static Reel CreateReel(string reel)
    {
        var reels = new Reel(reel);

        return reels;
    }

    public void Shuffle()
    {
        _currentIndex = new Random().Next(0, _reel.Length);
    }

    public char LookupNext()
    {
        return _reel[_currentIndex];
    }

    public void MoveNext() {
        _currentIndex = (_currentIndex + 1) % _reel.Length;
    }
}
