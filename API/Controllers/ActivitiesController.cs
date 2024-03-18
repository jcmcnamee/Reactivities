using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers;

public class ActivitiesController : BaseApiController
{
    private readonly DataContext _context;
    
    public ActivitiesController(DataContext context)
    {
            _context = context;
    }

    [HttpGet] //api/activities
    // Task<T>: This is a generic type representing an asynchronous operation that returns a value of type T.
    // In this case, T is ActionResult<List<Activity>>, which means the method returns a task that will eventually produce a
    // result of type ActionResult<List<Activity>>.
    // ActionResult<T>: This is a generic class representing the result of an action method in ASP.NET Core MVC.
    // It encapsulates both the HTTP status code and the data returned by the action. In this case, T is List<Activity>,
    // indicating that the action result will contain a list of activities.
    public async Task<ActionResult<List<Activity>>> GetActivities()
    {
        // Retrieve a list of activities from the database asynchronously,
        // then return the list of activities as an ActionResult with a HTTP 200 OK status code.
        return await _context.Activities.ToListAsync();
    }

    [HttpGet("{id}")] //api/activities/<uuid>
    public async Task<ActionResult<Activity>> GetActivity(Guid id) {
        return await _context.Activities.FindAsync(id);
    }

}
