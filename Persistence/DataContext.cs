
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

// DbContext is a class provided by Entity Framework Core that represents a session with the database
// and allows interaction with the database using entity classes.
// The Librarian (Database Context):
// Now, imagine you as the librarian. Your job is to manage all the books in the library, handle borrowing and returning books,
// and help people find what they need. In Entity Framework Core, the database context plays a similar role.
// It's like the librarian of your application's data.
public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }
    // DbSet<T> is a generic type provided by Entity Framework Core that represents a collection of entities of type T in the database.
    // Entities typically correspond to tables in a relational database, where each instance of an entity represents a row in the table.
    public DbSet<Activity> Activities {get; set;}
}   
