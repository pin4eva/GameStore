using GameStore.game.Dtos;

namespace GameStore.game.Entities;

public static class EntityExtensions
{
    public static GetGameDTO AsDto(this Game game)
    {
        return new GetGameDTO(
    game.Id,
    game.Name,
    game.ImageUri,
    game.Genre,
    game.Price,
    game.ReleaseDate
);
    }
}
