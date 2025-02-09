using Core;
using Core.Dtos;
using Core.Entities.Base;
using Core.Queries.Schedules;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.QueryHandlers.Schedules;

public class GetSchedulesQueryHandler: IRequestHandler<GetSchedulesQuery, PaginatedList<ScheduleDto>>
{
    private readonly WorkForceDbContext _context;

    public GetSchedulesQueryHandler(WorkForceDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<ScheduleDto>> Handle(GetSchedulesQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.Include(u => u.Schedules).ThenInclude(s => s.Workers)
            .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);
        
        if (user == null)
        {
            return new PaginatedList<ScheduleDto>(new List<ScheduleDto>(), 0, request.PageNumber, request.PageSize);
        }
        
        var query = user.Schedules.AsQueryable();
        
        var totalRecords = await query.CountAsync(cancellationToken);
        var schedules = await query
            .OrderBy(s => s.StartDate)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(s => new ScheduleDto
            {
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                Name = s.Name,
                Workers = s.Workers.Select(w => new WorkerDto
                {
                    FirstName = w.FirstName,
                    LastName = w.LastName,
                    ProfilePicture = w.ProfilePicture
                }).ToList()
            })
            .ToListAsync(cancellationToken);
        
        return new PaginatedList<ScheduleDto>(schedules, totalRecords, request.PageNumber, request.PageSize);
    }
}