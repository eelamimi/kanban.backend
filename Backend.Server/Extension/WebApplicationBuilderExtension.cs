namespace Backend.Server.Extension;

public static class WebApplicationBuilderExtension
{
    #region Database

    private static IServiceCollection AddDbContext(this IServiceCollection services, bool isDevelopment)
    {
        var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.MigrationsAssembly("Backend.Database");
                npgsqlOptions.CommandTimeout(30);

                npgsqlOptions.MapEnum<IssueType>();

                npgsqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(5),
                    errorCodesToAdd: null);
            });

            if (isDevelopment)
            {
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
                options.LogTo(Console.WriteLine, LogLevel.Information);
            }
        });

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserProfileRepository, UserProfileRepository>();
        services.AddScoped<ITeamRepository, TeamRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<ITeamUserProfileRepository, TeamUserProfileRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IColumnRepository, ColumnRepository>();
        services.AddScoped<IColumnRelationRepository, ColumnRelationRepository>();

        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, bool isDevelopment)
    {
        services.AddDbContext(isDevelopment);
        services.AddRepositories();

        return services;
    }

    #endregion

    #region Infrasrtucture

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, JwtSettings jwtSettings)
    {
        services.AddSingleton(jwtSettings);

        // Infrastructure services
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        
        // JWT settings
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,

                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Key)),

                    ValidateLifetime = true,
                    ClockSkew=TimeSpan.Zero,
                };
            });
        services.AddAuthorization();

        return services;
    }

    #endregion

    #region Application

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //services.AddScoped<IAuthService, AuthService>();
        //services.AddScoped<IUserService, UserService>();
        //services.AddScoped<IProjectService, ProjectService>();
        //services.AddScoped<ITeamService, TeamService>();
        //services.AddScoped<IColumnService, ColumnService>();

        return services;
    }

    #endregion
}
