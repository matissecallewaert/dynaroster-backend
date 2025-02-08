using MediatR;

namespace Core.Commands.users
{
    public class UpdateUserCommand(
        Guid Id,
        string FirstName,
        string LastName,
        string Email,
        string PhoneNumber,
        Address? Address,
        bool IsActive
    ) : IRequest<bool>
    {
        public Guid Id { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Address? Address { get; set; }
        public bool IsActive { get; set; }
    }
}
