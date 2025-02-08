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
            var user = await _context.Users.FindAsync(request.Id);
            if (user == null)
                return false;

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;
            user.Address = request.Address;
            user.IsActive = request.IsActive;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
