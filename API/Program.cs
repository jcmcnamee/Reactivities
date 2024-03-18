using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// In .NET, services are components that provide specific functionality to an application,
// such as database access, logging, or authentication.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// This method call registers a database context with the dependency injection (DI) container.
// In Entity Framework Core (EF Core), a database context represents a session with the database,
// allowing you to query and save data. DataContext is the specific type of the database context being registered.
builder.Services.AddDbContext<DataContext>(opt =>
{
    // This method configures the database provider that EF Core will use. In this case,
    // it specifies that the application will use SQLite as the database provider
    // The UseSqlite method requires a connection string to establish a connection to the SQLite database.
    // Connection strings contain information needed to connect to a database, such as the server address, credentials, and database name.
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

// After using this var it will be garbage collected due to the "using" statement.
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}

app.Run();
