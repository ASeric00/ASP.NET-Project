using ASeric00_lab_07.Repositories;
using ASeric00_lab_07.Logic;
using ASeric00_lab_07.Configuration;
using ASeric00_lab_07.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ErrorFilter>(); //Register ErrorFilter globally
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Repositories and their interfaces
builder.Services.AddSingleton<IWeatherRepository, WeatherSqlRepository>();
builder.Services.AddSingleton<IWeatherStationLogic, WeatherStationLogic>();

//Configuration
builder.Services.Configure<DBConfiguration>(builder.Configuration.GetSection("Database"));
builder.Services.Configure<ValidationConfiguration>(builder.Configuration.GetSection("Validation"));

builder.Services.AddCors(p => p.AddPolicy("cors_policy_allow_all", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("cors_policy_allow_all");

app.UseAuthorization();

app.MapControllers();

app.Run();
