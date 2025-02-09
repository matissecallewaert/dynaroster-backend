using Core;
using Core.Commands.users;
using MediatR;

namespace Application.Handlers.CommandHandlers.Users
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly WorkForceDbContext _context;

        public UpdateUserCommandHandler(WorkForceDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(
            UpdateUserCommand request,
            CancellationToken cancellationToken
        )
        {
            var user = await _context.Users.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);
            if (user == null)
                return false;

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;
            user.Address = request.Address;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
