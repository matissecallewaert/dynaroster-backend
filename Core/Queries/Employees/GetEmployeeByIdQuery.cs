using Core.Dtos;
using MediatR;

namespace Core.Queries.Employees;

public class GetEmployeeByIdQuery: IRequest<EmployeeDto>
{
    public Guid Id { get; set; } = Guid.Empty;
    public Guid ManagerId { get; set; } = Guid.Empty;
}