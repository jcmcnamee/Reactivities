using Application.Activities;
using Application.Activities.Core;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddSwaggerGen();
        services.AddEndpointsApiExplorer();

        // This method call registers a database context with the dependency injection (DI) container.
        // In Entity Framework Core (EF Core), a database context represents a session with the database,
        // allowing you to query and save data. DataContext is the specific type of the database context being registered.
        services.AddDbContext<DataContext>(opt =>
        {
            // This method configures the database provider that EF Core will use. In this case,
            // it specifies that the application will use SQLite as the database provider
            // The UseSqlite method requires a connection string to establish a connection to the SQLite database.
            // Connection strings contain information needed to connect to a database, such as the server address, credentials, and database name.
            opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
        });

        // Build CORS middleware
        services.AddCors(opt => {
            opt.AddPolicy("CorsPolicy", policy => 
            {
                policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:5173");
            });
        });

        // Add MediatR and tell it where to find the handlers we're creating..
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(List.Handler).Assembly));
        services.AddAutoMapper(typeof(MappingProfiles).Assembly);

        return services;
    }
}
