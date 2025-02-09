using Core.Dtos;
using Core.Entities.Base;
using MediatR;

namespace Core.Queries.Schedules;

public class GetSchedulesQuery : IRequest<PaginatedList<ScheduleDto>>
{
    public Guid Id { get; set; } = Guid.Empty;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}