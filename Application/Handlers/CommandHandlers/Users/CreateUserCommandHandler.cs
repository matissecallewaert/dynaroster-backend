using Core;
using Core.Commands.users;
using MediatR;

namespace Application.Handlers.CommandHandlers.Users
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly WorkForceDbContext _context;

        public CreateUserCommandHandler(WorkForceDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(
            CreateUserCommand request,
            CancellationToken cancellationToken
        )
        {
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                PasswordHash = request.PasswordHash,
                Role = request.Role,
                Address = request.Address,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);
            return user.Id;
        }
    }
}
