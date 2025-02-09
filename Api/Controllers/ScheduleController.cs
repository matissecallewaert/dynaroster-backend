using Application.Checks;
using Core.Commands.Schedules;
using Core.Queries.Schedules;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WorkforcePlanner.Api.Controllers;

[ApiController]
[Route("api/schedules")]
public class ScheduleController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetSchedules([FromQuery] GetSchedulesQuery query)
    {
        if (query.Id == Guid.Empty)
            return BadRequest("Invalid request.");
        
        if(!IsSameUser.CheckIsSameUser(User, query.Id))
            return Forbid(JwtBearerDefaults.AuthenticationScheme);
        
        var result = await mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> GetScheduleById([FromRoute] Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest("Invalid request.");
        
        if(!IsSameUser.CheckIsSameUser(User, id))
            return Forbid(JwtBearerDefaults.AuthenticationScheme);
        
        var query = new GetScheduleByIdQuery { Id = id };
        var result = await mediator.Send(query);
        return Ok(result);
    }
    
    [HttpDelete("{id:guid}")]
    [Authorize (Roles = "Manager")]
    public async Task<IActionResult> DeleteSchedule([FromRoute] Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest("Invalid request.");

        var userId = User.FindFirst("sub")?.Value;
        if (userId == null) return Forbid(JwtBearerDefaults.AuthenticationScheme);
        
        var command = new DeleteScheduleByIdCommand { Id = id, UserId = Guid.Parse(userId)};
        var result = await mediator.Send(command);
        return Ok(result);

    }
    
}