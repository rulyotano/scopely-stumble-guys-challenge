using System.Threading.Tasks;

namespace ReelWords.Domain.Entities.Game;

public interface IScoresProvider
{
    public Task<Scores> GetScores();
}
