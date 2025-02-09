using Core.Dtos;
using Core.Entities.Base;
using MediatR;

namespace Core.Queries.Employees;

public class GetEmployeesQuery : IRequest<PaginatedList<EmployeeDto>>
{
    public Guid ManagerId { get; set; } = Guid.Empty;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}