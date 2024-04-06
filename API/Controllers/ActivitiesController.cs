using Application.Activities;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ActivitiesController : BaseApiController
{
    [HttpGet] //api/activities
    // Task<T>: This is a generic type representing an asynchronous operation that returns a value of type T.
    // In this case, T is ActionResult<List<Activity>>, which means the method returns a task that will eventually produce a
    // result of type ActionResult<List<Activity>>.
    // ActionResult<T>: This is a generic class representing the result of an action method in ASP.NET Core MVC.
    // It encapsulates both the HTTP status code and the data returned by the action. In this case, T is List<Activity>,
    // indicating that the action result will contain a list of activities.
    public async Task<ActionResult<List<Activity>>> GetActivities(CancellationToken ct)
    {
        // Retrieve a list of activities from the database asynchronously,
        // then return the list of activities as an ActionResult with a HTTP 200 OK status code.
        // return await _context.Activities.ToListAsync();

        // Using MediatR:
        return await Mediator.Send(new List.Query());
    }

    [HttpGet("{id}")] //api/activities/<uuid>
    public async Task<ActionResult<Activity>> GetActivity(Guid id) {
        // return await _context.Activities.FindAsync(id);
        return await Mediator.Send(new Details.Query{Id = id});
    }

    [HttpPost]
    // We are not returning a Task type, so we can return IActionResult. This gives us action to http response types.
    public async Task<IActionResult> CreateActivity(Activity activity)
    {
        Console.WriteLine("hello!");
        await Mediator.Send(new Create.Command {Activity = activity});
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditActivity(Guid id, Activity activity)
    {
        activity.Id = id;
        await Mediator.Send(new Edit.Command {Activity = activity});
        return Ok();
    }

    [HttpDelete("{id}")]
    // Again, no type required
    public async Task<IActionResult> DeleteActivity(Guid id)
    {
        await Mediator.Send(new Delete.Command{Id=id});

        return Ok();
    }


}
