using Core;
using Core.Commands.Schedules;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.CommandHandlers.Schedules;

public class DeleteScheduleByIdCommandHandler : IRequestHandler<DeleteScheduleByIdCommand, bool>
{
    private readonly WorkForceDbContext _context;

    public DeleteScheduleByIdCommandHandler(WorkForceDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> Handle(
        DeleteScheduleByIdCommand request,
        CancellationToken cancellationToken
    )
    {
        var userWithSchedules = await _context.Users
            .Include(u => u.Schedules)
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        var schedule = userWithSchedules?.Schedules.FirstOrDefault(s => s.Id == request.Id);
        
        if (schedule == null)
            return false;
        
        userWithSchedules?.Schedules.Remove(schedule);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}