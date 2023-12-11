using GameStore.game.Entities;

namespace GameStore.game.Repositories;

public interface IGameRepository
{
    IEnumerable<Game> FindAll();
    Game? FindOneById(int id);
    Game Create(Game game);

    void UpdateOne(Game game);
    void DeleteOne(int id);
}
