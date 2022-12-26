using ReelWords.Domain.Entities.Game;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace ReelWords.Game;

public class ReelsProvider : IReelsProvider
{
    public const string ReelsPathKey = "ReelsPath";

    public async Task<ReelCollection> GetReelsForPlayer(string playerId)
    {
        var reelsPath = ConfigurationManager.AppSettings.Get(ReelsPathKey);
        var reels = new List<string>();
        using (var streamReader = new StreamReader(reelsPath))
        {
            var line = await streamReader.ReadLineAsync();
            while (line != null)
            {
                reels.Add(line);
                line = await streamReader.ReadLineAsync();
            }
        }

        return ReelCollection.CreateReelCollection(reels);
    }
}
