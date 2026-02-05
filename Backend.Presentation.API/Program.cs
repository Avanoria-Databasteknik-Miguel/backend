using Backend.Presentation.API.Dtos;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddCors();

var app = builder.Build();


app.MapOpenApi();

app.UseHttpsRedirection();
app.UseCors(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


app.MapGet("/api/heroes", () =>
{



    var heroes = new List<HeroDto>
    {
        new(
        1, 
        "Deam big. Achieve more. Visual Studio 2026", 
        "Unleash our full potential with the world's most polular IDE for the professional developer"
        )
    };

    return Results.Ok(heroes);
});



app.Run();
