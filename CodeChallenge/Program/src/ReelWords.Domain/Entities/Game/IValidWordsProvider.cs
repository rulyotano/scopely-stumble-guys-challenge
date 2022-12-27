using System.Threading.Tasks;

namespace ReelWords.Domain.Entities.Game;

public interface IValidWordsProvider
{
    public Task<ValidWords> GetValidWords();
}
