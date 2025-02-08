using MediatR;

namespace Core.Commands.users
{
    public record DeleteUserCommand(Guid Id) : IRequest<bool>
    {
        public Guid Id { get; init; }
    }
}
