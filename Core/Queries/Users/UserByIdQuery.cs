using MediatR;

namespace Core.Queries.Users;

public class UserByIdQuery(Guid id) : IRequest<User>
{
    public Guid Id { get; set; }
}
