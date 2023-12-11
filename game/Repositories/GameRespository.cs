

using GameStore.Data;
using GameStore.game.Dtos;
using GameStore.game.Entities;
using GameStore.game.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Repositories;

public class GameRespository(DataContext db) : IGameRepository
{

    private readonly DataContext db = db;

    public async Task<IEnumerable<Game>> FindAll()
    {
        return await db.games.AsNoTracking().ToListAsync();
    }

    public async Task<Game?> FindOneById(int id)
    {
        return await db.games.FirstOrDefaultAsync(game => game.Id == id);
    }

    public async Task<Game> Create(CreateGameDTO newGame)
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
        await db.SaveChangesAsync();

        return game;
    }

    public async Task UpdateOne(UpdateGameDTO updatedGame)
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
            await db.SaveChangesAsync();
        }



    }

    public async Task DeleteOne(int id)

    {
        await db.games.Where(game => game.Id == id)
            .ExecuteDeleteAsync();
    }

}
