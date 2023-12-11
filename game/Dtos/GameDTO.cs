using System.ComponentModel.DataAnnotations;

namespace GameStore.game.Dtos;

public record GetGameDTO(
int Id,
string Name,
string Genre,
string ImageUri,
Decimal Price,
DateTime ReleaseDate
);


public record CreateGameDTO(
 [Required, StringLength(50)] string Name,
 [Required, StringLength(50)] string Genre,
 [Required, StringLength(50), Url] string ImageUri,
[Range(1, 100)] Decimal Price,
DateTime ReleaseDate
);


public record UpdateGameDTO(
int Id,
string Name,
string Genre,
string ImageUri,
Decimal Price,
DateTime ReleaseDate
);


