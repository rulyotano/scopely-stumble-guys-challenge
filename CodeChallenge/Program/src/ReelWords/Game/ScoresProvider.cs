using ReelWords.Domain.Entities.Game;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace ReelWords.Game;

public class ScoresProvider : IScoresProvider
{
    public const string ScoresPathKey = "ScoresPath";

    public async Task<Scores> GetScores()
    {
        var scoresPath = ConfigurationManager.AppSettings.Get(ScoresPathKey);
        var scores = new List<(char, int)>();
        using (var streamReader = new StreamReader(scoresPath))
        {
            var line = await streamReader.ReadLineAsync();
            while (line != null)
            {
                var lineSplitted = line.Split(' ');
                scores.Add((lineSplitted[0][0], int.Parse(lineSplitted[1])));
                line = await streamReader.ReadLineAsync();
            }
        }

        return Scores.CreateScores(scores);
    }
}
