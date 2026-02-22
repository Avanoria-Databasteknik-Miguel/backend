var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();

var app = builder.Build();


app.MapOpenApi();

app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());


app.Run();
