using Core;
using Core.Commands.users;
using MediatR;

namespace Application.Handlers.CommandHandlers.Users
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly WorkForceDbContext _context;

        public DeleteUserHandler(WorkForceDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(
            DeleteUserCommand request,
            CancellationToken cancellationToken
        )
        {
            var user = await _context.Users.FindAsync(request.Id);
            if (user == null)
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
