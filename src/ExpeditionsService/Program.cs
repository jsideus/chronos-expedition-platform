var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Urls.Add("http://localhost:5080");

app.MapGet("/expeditions/{id:int}", (int id) =>
    id == 1
        ? Results.Ok(new ExpeditionResponse
        {
            Id = 1,
            Name = "Library of Alexandria",
            Era = "Classical",
            Year = -48,
            MaxPartySize = 6,
        })
        : Results.NotFound());

app.Run();

internal sealed record ExpeditionResponse
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required string Era { get; init; }
    public required int Year { get; init; }
    public required int MaxPartySize { get; init; }
}
