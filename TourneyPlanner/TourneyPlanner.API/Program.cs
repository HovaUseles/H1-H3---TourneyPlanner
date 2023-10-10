using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TourneyPlanner.API.DTOs;
using TourneyPlanner.API.Models;
using TourneyPlanner.API.Repositories;
using TourneyPlanner.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserRepository, SqlUserRepository>();
builder.Services.AddScoped<ITournamentRepository, SqlTournamentRepository>();
builder.Services.AddScoped<IMatchupRepository, SqlMatchupRepository>();

builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<ISaltService, SaltService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddDbContext<TourneyPlannerDevContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("TourneyPlanner"));
    options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddFilter(level => level >= LogLevel.Warning)));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();