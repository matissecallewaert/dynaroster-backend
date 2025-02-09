using System.Security.Claims;
using Application.Checks;
using Core.Queries.Employees;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WorkforcePlanner.Api.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeeController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Authorize (Roles = "SuperAdmin, Manager")]
        public async Task<IActionResult> GetEmployees([FromBody] GetEmployeesQuery query)
        {
            if (query.ManagerId == Guid.Empty)
                return BadRequest("Invalid request.");
            
            if (!IsSameUser.CheckIsSameUser(User, query.ManagerId))
                return Forbid(JwtBearerDefaults.AuthenticationScheme);
            
            var result = await mediator.Send(query);
            return Ok(result);
        }
        
        [HttpGet("{id:guid}")]
        [Authorize (Roles = "SuperAdmin, Manager")]
        public async Task<IActionResult> GetEmployee(Guid id)
        {
            var value = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (value == null) return Forbid(JwtBearerDefaults.AuthenticationScheme);
            
            var query = new GetEmployeeByIdQuery
            {
                Id = id,
                ManagerId = Guid.Parse(value)
            };
            var result = await mediator.Send(query);
            return Ok(result);
        }
    }
}