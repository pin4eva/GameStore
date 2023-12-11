

using GameStore.game.Entities;
using GameStore.game.Repositories;

namespace GameStore.Repositories;

public class GameRespository : IGameRepository
{
    private readonly List<Game> games = [
    new Game()
{
Id = 1,
Name = "Street Fighter II",
Genre = "Fighting",
Price = 19.99M,
ReleaseDate = new DateTime(1991,2,1),
ImageUri = "https://placeholder.co/100"
}
    ];


    public IEnumerable<Game> FindAll()
    {
        return games;
    }

    public Game? FindOneById(int id)
    {
        return games.FirstOrDefault(game => game.Id == id);
    }

    public Game Create(Game game)
    {
        var Id = games.Max(game => game.Id);
        game.Id = Id + 1;
        games.Add(game);
        return game;
    }

    public void UpdateOne(Game updatedGame)
    {
        var game = games.FirstOrDefault(game => game.Id == updatedGame.Id);



        if (game is not null)
        {
            game = updatedGame;
        }


    }

    public void DeleteOne(int id)

    {
        var game = games.FirstOrDefault(game => game.Id == id);



        if (game is not null)
        {
            games.Remove(game);
        }
    }

}
