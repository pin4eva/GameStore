using GameStore.game.Entities;
using GameStore.game.Repositories;

namespace GameStore.game.Endpoints;
public static class GameEndpoint
{
    public static RouteGroupBuilder MapGamesEndpoint(this IEndpointRouteBuilder routeBuilder)
    {


        var gameGroup = routeBuilder.MapGroup("/games")
.WithParameterValidation()
.WithOpenApi();

        gameGroup.MapGet("/", (IGameRepository repository) =>
        {

            return repository.FindAll();
        });


        gameGroup.MapGet("/{id}", (IGameRepository repository, int id) =>
        {

            var game = repository.FindOneById(id);
            if (game is null) return Results.NotFound("Invalid game id");

            return Results.Ok(game);
        })
        .WithName("GetGame");

        gameGroup.MapPost("/", (IGameRepository repository, Game game) =>
        {
            var newGame = repository.Create(game);
            return Results.CreatedAtRoute("GetGame", new { Id = newGame.Id }, newGame);
        });

        gameGroup.MapPut("/", (IGameRepository repository, Game updatedGame) =>
        {
            var game = repository.FindOneById(updatedGame.Id);
            if (game is null) return Results.NotFound("Invalid game id");

            game.Price = updatedGame.Price;
            game.ImageUri = updatedGame.ImageUri;
            game.ReleaseDate = updatedGame.ReleaseDate;
            game.Name = updatedGame.Name;

            repository.UpdateOne(game);

            return Results.Ok(game);
        });

        gameGroup.MapDelete("/{id}", (IGameRepository repository, int id) =>
        {

            var game = repository.FindOneById(id);
            if (game is null) return Results.NotFound("Invalid game id");
            repository.DeleteOne(id);

            return Results.Ok(game);
        });

        return gameGroup;
    }
}
