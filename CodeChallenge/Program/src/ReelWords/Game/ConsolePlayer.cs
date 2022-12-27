using ReelWords.Domain.Entities;
using ReelWords.Domain.Entities.Game;
using System;

namespace ReelWords.Game;

public class ConsolePlayer : IPlayer
{
    private const string Help = "help";
    private const string Points = "points";
    private const string Letters = "letters";
    private const string Submit = "submit";
    private const string Quit = "quit";

    public string Id => "ConsolePlayer";

    public void Play(IGameManager gameManager)
    {
        ShowWelcome(gameManager);
        while (true)
        {
            var option = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(option)) return;
            var optionSplitted = option.Split(' ');
            var command = optionSplitted[0];

            switch (command)
            {
                case Help:
                    ShowOptions();
                    break;
                case Points:
                    ShowPoints(gameManager);
                    break;
                case Letters:
                    ShowLetters(gameManager);
                    break;
                case Submit:
                    SubmitWord(gameManager, optionSplitted);
                    break;
                case Quit:
                    return;
                default:
                    ShowOptions();
                    break;
            }
        }
    }

    private void ShowOptions()
    {
        Console.WriteLine(@"Options:
'help'            Show this menu
'points'          Show current points
'submit <word>'   Submit word
'letters'         Letters you have in the reels
'quit'            Finish the game");
    }

    private void ShowWelcome(IGameManager gameManager)
    {
        Console.WriteLine(@$"Welcome to the 'Reel Words' game. Press 'help' to see all available options.
You started the game with the following available letters to play: {GetLettersFromReels(gameManager)}");
    }

    private void ShowPoints(IGameManager gameManager)
    {
        var points = gameManager.GetPoints(Id);
        Console.WriteLine($"So far you've earned {points} points.");
    }

    private void ShowLetters(IGameManager gameManager) => Console.WriteLine($"You can create any word with the following letters: '{GetLettersFromReels(gameManager)}'.");

    private string GetLettersFromReels(IGameManager gameManager)
    {
        var availableLetters = gameManager.GetValidCharacters(Id);
        return string.Join(" | ", availableLetters).ToUpper();
    }

    private void SubmitWord(IGameManager gameManager, string[] optionSplitted)
    {
        if (optionSplitted.Length < 2)
        {
            Console.WriteLine("You should provide a word to submit.");
            return;
        }
        var word = optionSplitted[1];
        var previousScore = gameManager.GetPoints(Id);
        var playResult = gameManager.PlayWord(Id, word);

        switch (playResult)
        {   
            case PlayWordResult.Success:
                var newScore = gameManager.GetPoints(Id);
                Console.WriteLine(@$"Good job! Word accepted. You've won {newScore - previousScore} points. Your new score is '{newScore}'.
Now you can use: {GetLettersFromReels(gameManager)}");
                break;
            case PlayWordResult.NotExistingWord:
                Console.WriteLine($"Word '{word}' doesn't exist in this game's dictionary.");
                break;
            case PlayWordResult.NotWordInReel:
                Console.WriteLine($"You can't play '{word}' with your available letters. Use 'letters' to see the ones you can use.");
                break;
        }
    }
}
