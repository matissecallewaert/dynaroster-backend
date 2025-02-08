using Core;
using Core.Queries.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.QueryHandlers.Users
{
    public class GetUserByIdQueryHandler : IRequestHandler<UserByIdQuery, User>
    {
        private readonly WorkForceDbContext _context;

        public GetUserByIdQueryHandler(WorkForceDbContext context)
        {
            _context = context;
        }

        public async Task<User> Handle(UserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users.FirstOrDefaultAsync(
                u => u.Id == request.Id,
                cancellationToken
            );
        }
    }
}
