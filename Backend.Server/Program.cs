var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// MY DI
var frontendName = "ReactApp";
var jwtSettings = new JwtSettings();
builder.Configuration.GetSection("Jwt").Bind(jwtSettings);

builder.Services
    .AddDatabase(builder.Environment.IsDevelopment())
    .AddInfrastructure(jwtSettings)
    .AddApplication()
    .AddCors(options =>
    {
        options.AddPolicy(frontendName, policy =>
        {
            policy
            .WithOrigins("http://localhost:5173", "http://localhost:4173")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
    });

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// DB Migrations
await app.MakeMigrationsAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseMiddleware<GlobalExceptionHandler>();

//app.UseHttpsRedirection();

app.UseCors(frontendName);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
