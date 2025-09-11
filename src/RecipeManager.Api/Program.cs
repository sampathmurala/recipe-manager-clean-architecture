using RecipeManager.Core.Gateways;
using RecipeManager.Core.UseCases;
using RecipeManager.Data.Sqlite;
using RecipeManager.Data.Sqlite.Gateways;

var builder = WebApplication.CreateBuilder(args);

// --- CORS Configuration ---
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // Allow requests from our React app http://localhost:5173
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
// --- End of CORS Configuration ---

// Add services to the container.
builder.Services.AddControllers();

//----- dependency injection-----
builder.Services.AddSingleton<IRecipeGateway, SqliteRecipeGateway>();
builder.Services.AddScoped<CreateRecipeUseCase>();
builder.Services.AddScoped<GetRecipesUseCase>();


builder.Services.AddEndpointsApiExplorer();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// --- CORS Middleware ---
app.UseCors();
// --- End of CORS Middleware ---

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast =  Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast");

app.Run();

//record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
//{
//    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}
