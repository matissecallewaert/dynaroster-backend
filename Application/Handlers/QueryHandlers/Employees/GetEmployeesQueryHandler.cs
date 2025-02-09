using Core;
using Core.Dtos;
using Core.Entities.Base;
using Core.Queries.Employees;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.QueryHandlers.Employees;

public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, PaginatedList<EmployeeDto>>
{
    private readonly WorkForceDbContext _context;

    public GetEmployeesQueryHandler(WorkForceDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<EmployeeDto>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
    {
        var manager = await _context.Managers.Include(m => m.Workers)
            .FirstOrDefaultAsync(m => m.Id == request.ManagerId, cancellationToken);
        
        if (manager == null)
        {
            return new PaginatedList<EmployeeDto>(new List<EmployeeDto>(), 0, request.PageNumber, request.PageSize);
        }

        var query = _context.Workers.Where(w => w.ManagerId == request.ManagerId);

        var totalRecords = await query.CountAsync(cancellationToken);
        var employees = await query
            .OrderBy(e => e.LastName)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(e => new EmployeeDto
            {
                Id = e.Id.ToString(),
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                ProfilePicture = e.ProfilePicture
            })
            .ToListAsync(cancellationToken);

        return new PaginatedList<EmployeeDto>(employees, totalRecords, request.PageNumber, request.PageSize);
    }
}