

using GameStore.Data;
using GameStore.game.Dtos;
using GameStore.game.Entities;
using GameStore.game.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Repositories;

public class GameRespository : IGameRepository
{

    private readonly DataContext db;

    public GameRespository(DataContext db)
    {
        this.db = db;
    }

    public IEnumerable<Game> FindAll()
    {
        return db.games.AsNoTracking().ToList();
    }

    public Game? FindOneById(int id)
    {
        return db.games.FirstOrDefault(game => game.Id == id);
    }

    public Game Create(CreateGameDTO newGame)
    {
        Game game = new()
        {
            Name = newGame.Name,
            Genre = newGame.Genre,
            ImageUri = newGame.ImageUri,
            Price = newGame.Price,
            ReleaseDate = newGame.ReleaseDate
        };
        db.games.Add(game);
        db.SaveChanges();

        return game;
    }

    public void UpdateOne(UpdateGameDTO updatedGame)
    {
        var game = db.games.FirstOrDefault(game => game.Id == updatedGame.Id);



        if (game is not null)
        {
            game.Name = updatedGame.Name;
            game.Genre = updatedGame.Genre;
            game.ImageUri = updatedGame.ImageUri;
            game.Price = updatedGame.Price;
            game.ReleaseDate = updatedGame.ReleaseDate;
            db.games.Update(game);
            db.SaveChanges();
        }



    }

    public void DeleteOne(int id)

    {
        db.games.Where(game => game.Id == id).ExecuteDelete();




    }

}
