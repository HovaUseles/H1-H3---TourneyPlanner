using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TourneyPlanner.API.Models;
using TourneyPlanner.API.Repositories;
using TourneyPlanner.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "https://localhost:7127",
        ValidAudience = "https://localhost:7127",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtKey"]!)),
    };
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("V1", new OpenApiInfo
    {
        Version = "V1",
        Title = "GraphFlix API",
        Description = "GraphFlix API"
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

// Repositories
builder.Services.AddScoped<IUserRepository, SqlUserRepository>();
builder.Services.AddScoped<ITournamentRepository, SqlTournamentRepository>();
builder.Services.AddScoped<IMatchupRepository, SqlMatchupRepository>();
builder.Services.AddScoped<ITeamRepository, SqlTeamRepository>();

// Services
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<ISaltService, SaltService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddSingleton<INotificationService, NotificationService>();

builder.Services.AddDbContext<TourneyPlannerDevContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("TourneyPlanner"));
    options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddFilter(level => level >= LogLevel.Warning)));
});

string corsPolicy = "_allow";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicy,
                      policy =>
                      {
                          policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
                      });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/V1/swagger.json", "TourneyPlanner API");
    });
}

app.UseHttpsRedirection();

app.UseCors(corsPolicy);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

//test.com    app.Run();