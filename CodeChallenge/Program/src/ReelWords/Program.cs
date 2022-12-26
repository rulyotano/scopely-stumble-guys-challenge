using ReelWords.Domain.Entities.Game;
using ReelWords.Game;
using System.Threading.Tasks;

namespace ReelWords
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            var gameManager = await GameManager.CreateGameManager(new ReelsProvider(), new ScoresProvider(), new ValidWordsProvider());
            var consolePlayer = new ConsolePlayer();

            await gameManager.InitializeGame(new string[] { consolePlayer.Id });

            consolePlayer.Play(gameManager);
        }
    }
}