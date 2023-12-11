using GameStore.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

List<Game> games =
[
    new Game()
    {
        Id = 1,
        Name = "Street fighter II",
        Genre = "Fighting",
        Price = 19.99M,
        ReleaseDate = new DateTime(1991,2,1),
        ImageUri = "https://placeholder.co/100"
    },
new Game()
{
    Id = 2,
    Name = "Final Fantacy",
    Genre = "Roleplaying",
    Price = 59.99M,
    ReleaseDate = new DateTime(2010,9,30),
    ImageUri = "https://placeholder.co/100"
}
];

var gameGroup = app.MapGroup("/games")
.WithParameterValidation()
.WithOpenApi();

gameGroup.MapGet("/", () =>
{

    return games;
});


gameGroup.MapGet("/{id}", (int id) =>
{

    var game = games.FirstOrDefault((game) => game.Id == id);
    if (game is null) return Results.NotFound("Invalid game id");

    return Results.Ok(game);
})
.WithName("GetGame");

gameGroup.MapPost("/", (Game game) =>
{
    var Id = games.Max(game => game.Id);
    game.Id = Id + 1;
    games.Add(game);
    return Results.CreatedAtRoute("GetGame", new { Id }, game);
});

gameGroup.MapPut("/", (Game updatedGame) =>
{
    var game = games.FirstOrDefault(game => game.Id == updatedGame.Id);

    if (game is null) return Results.NotFound("Invalid game id");

    game.Price = updatedGame.Price;
    game.ImageUri = updatedGame.ImageUri;
    game.ReleaseDate = updatedGame.ReleaseDate;
    game.Name = updatedGame.Name;

    return Results.Ok(game);
});

gameGroup.MapDelete("/{id}", (int id) =>
{

    var game = games.FirstOrDefault((game) => game.Id == id);
    if (game is null) return Results.NotFound("Invalid game id");

    games.Remove(game);

    return Results.Ok(game);
});

app.Run();

