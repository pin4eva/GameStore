using GameStore.game.Dtos;
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

        gameGroup.MapGet("/", async (IGameRepository repository) =>
        {

            var games = await repository.FindAll();
            games.Select((game) => game.AsDto());
            return games;
        });


        gameGroup.MapGet("/{id}", async (IGameRepository repository, int id) =>
        {

            var game = await repository.FindOneById(id);
            if (game is null) return Results.NotFound("Invalid game id");

            return Results.Ok(game.AsDto());
        })
        .WithName("GetGame");

        gameGroup.MapPost("/", async (IGameRepository repository, CreateGameDTO game) =>
        {
            var newGame = await repository.Create(game);
            return Results.CreatedAtRoute("GetGame", new { Id = newGame.Id }, newGame);
        });

        gameGroup.MapPut("/", async (IGameRepository repository, UpdateGameDTO updatedGame) =>
        {
            var game = await repository.FindOneById(updatedGame.Id);
            if (game is null) return Results.NotFound("Invalid game id");



            await repository.UpdateOne(updatedGame);

            return Results.Ok(game);
        });

        gameGroup.MapDelete("/{id}", async (IGameRepository repository, int id) =>
        {

            var game = await repository.FindOneById(id);
            if (game is null) return Results.NotFound("Invalid game id");
            await repository.DeleteOne(id);

            return Results.Ok(game);
        });

        return gameGroup;
    }
}
