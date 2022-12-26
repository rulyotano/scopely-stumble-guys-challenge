using ReelWords.Domain.Entities.Game;

namespace ReelWords.Domain.Entities;

public interface IPlayer
{
    string Id { get; }

    void Play(IGameManager gameManager);
}
