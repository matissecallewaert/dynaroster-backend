using MediatR;

namespace Core.Commands.users
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public Guid Id { get; init; }
    }
}
