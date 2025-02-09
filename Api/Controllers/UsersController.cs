using Application.Checks;
using Core.Commands.users;
using Core.Commands.Users;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WorkforcePlanner.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(new { Token = result });
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserCommand command)
        {
            if (!CheckQueryIdSameAsBodyId.CheckIdSameAsBodyId(id, command.Id))
                return BadRequest("Invalid update request.");

            if (!IsSameUser.CheckIsSameUser(User, id))
                return Forbid(JwtBearerDefaults.AuthenticationScheme);

            var result = await mediator.Send(command);
            return result ? Ok("User updated successfully.") : NotFound("User not found.");
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            if (!IsSameUser.CheckIsSameUser(User, id))
                return Forbid(JwtBearerDefaults.AuthenticationScheme);

            var command = new DeleteUserCommand
            {
                Id = id
            };
            var result = await mediator.Send(command);
            return result ? Ok("User deleted successfully.") : NotFound("User not found.");
        }
        
        [HttpPost("{id}/upload-profile-picture")]
        public async Task<IActionResult> UploadProfilePicture(Guid id, IFormFile file)
        {
            if(!IsSameUser.CheckIsSameUser(User, id))
                return Forbid(JwtBearerDefaults.AuthenticationScheme);
            
            switch (file.Length)
            {
                case 0:
                    return BadRequest("Invalid file.");
                // Limit file size to 5MB
                case > 5 * 1024 * 1024:
                    return BadRequest("File too large. Max size is 5MB.");
            }

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var imageBytes = memoryStream.ToArray();

            var command = new UploadProfilePictureCommand
            {
                Id = id,
                Image = imageBytes
            };
            var result = await mediator.Send(command);

            return result? Ok("Profile picture updated successfully.") : NotFound("User not found.");
        }

    }
}