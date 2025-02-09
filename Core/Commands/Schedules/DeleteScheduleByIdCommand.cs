using MediatR;

namespace Core.Commands.Schedules;

public class DeleteScheduleByIdCommand: IRequest<bool>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
}