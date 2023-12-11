using GameStore.game.Dtos;
using GameStore.game.Entities;

namespace GameStore.game.Repositories;

public interface IGameRepository
{
    Task<IEnumerable<Game>> FindAll();
    Task<Game?> FindOneById(int id);
    Task<Game> Create(CreateGameDTO game);

    Task UpdateOne(UpdateGameDTO game);
    Task DeleteOne(int id);
}
