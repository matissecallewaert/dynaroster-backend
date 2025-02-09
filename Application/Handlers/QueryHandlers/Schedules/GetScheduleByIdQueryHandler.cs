using Core;
using Core.Entities;
using Core.Queries.Schedules;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.QueryHandlers.Schedules;

public class GetScheduleByIdQueryHandler : IRequestHandler<GetScheduleByIdQuery, Schedule>
{
    private readonly WorkForceDbContext _context;

    public GetScheduleByIdQueryHandler(WorkForceDbContext context)
    {
        _context = context;
    }
    
    public async Task<Schedule> Handle(GetScheduleByIdQuery request, CancellationToken cancellationToken)
    {
        var schedule = await _context.Schedules.FirstOrDefaultAsync(
            u => u.Id == request.Id,
            cancellationToken
        );
            
        if (schedule == null)
        {
            throw new Exception("Schedule not found.");
        }
            
        return schedule;
    }
}