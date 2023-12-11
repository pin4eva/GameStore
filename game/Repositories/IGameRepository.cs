using GameStore.game.Dtos;
using GameStore.game.Entities;

namespace GameStore.game.Repositories;

public interface IGameRepository
{
    IEnumerable<Game> FindAll();
    Game? FindOneById(int id);
    Game Create(CreateGameDTO game);

    void UpdateOne(UpdateGameDTO game);
    void DeleteOne(int id);
}
