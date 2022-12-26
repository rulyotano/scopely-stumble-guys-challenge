using System.Threading.Tasks;

namespace ReelWords.Domain.Entities.Game;

public interface IReelsProvider
{
    public Task<ReelCollection> GetReelsForPlayer(string playerId);
}
