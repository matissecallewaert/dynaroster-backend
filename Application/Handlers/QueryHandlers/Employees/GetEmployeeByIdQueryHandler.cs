using Core;
using Core.Dtos;
using Core.Entities;
using Core.Enums;
using Core.Queries.Employees;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.QueryHandlers.Employees;

public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto>
{
    private readonly WorkForceDbContext _context;

    public GetEmployeeByIdQueryHandler(WorkForceDbContext context)
    {
        _context = context;
    }
    
    public async Task<EmployeeDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var employee = await _context.Users.FirstOrDefaultAsync(
            u => u.Id == request.Id,
            cancellationToken
        );

        if (employee == null)
        {
            throw new Exception("Employee not found.");
        }
        
        if(employee.Role != UserRole.Worker)
        {
            throw new Exception("User is not an employee.");
        }

        var employeeWorker = (Worker)employee;
        
        if (employeeWorker == null)
        {
            throw new Exception("Employee not found.");
        }
        
        if(employeeWorker.ManagerId != request.ManagerId)
        {
            throw new Exception("Employee not found.");
        }

        return new EmployeeDto
        {
            Id = employee.Id.ToString(),
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber,
            ProfilePicture = employee.ProfilePicture
        };
    }
}