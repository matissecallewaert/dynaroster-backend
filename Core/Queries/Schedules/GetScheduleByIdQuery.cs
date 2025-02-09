using Core.Entities;
using MediatR;

namespace Core.Queries.Schedules;

public class GetScheduleByIdQuery: IRequest<Schedule>
{
    public Guid Id { get; set; }
}