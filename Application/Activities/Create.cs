using Domain;
using MediatR;
using Persistence;

namespace Application.Activities;

public class Create
{
    public class Command : IRequest
    {
        public Activity Activity { get; set; }
    }

    public class Handler : IRequestHandler<Command>
    {
        private readonly DataContext _context;
        public Handler(DataContext context)
        {
            _context = context;

        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            // Use EF to cache the change to memory
            _context.Activities.Add(request.Activity);

            // Commit the change
            await _context.SaveChangesAsync();

            // Return request has finished to API
            // return Unit.Value; Old MediatR thing...

        }
    }
}
