using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.Activities;

public class List
{
    // Mediator query
    public class Query : IRequest<List<Activity>> {}

    public class Handler : IRequestHandler<Query, List<Activity>>
    {
        private readonly DataContext _context;
        private readonly ILogger<List> _logger;
        public Handler(DataContext context)
        {
            _context = context;
        }
        // Task represents an async operation that returns a result
        // In this case the method will return a list of Activity objects wrapped in a Task.
        public async Task<List<Activity>> Handle(Query request, CancellationToken token)
        {
            return await _context.Activities.ToListAsync();
        }
    }
}
