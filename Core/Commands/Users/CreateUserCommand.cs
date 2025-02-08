using Core.Enums;
using MediatR;

namespace Core.Commands.users
{
    public class CreateUserCommand(
        string FirstName,
        string LastName,
        string Email,
        string PhoneNumber,
        string PasswordHash,
        UserRole Role,
        Address? Address
    ) : IRequest<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; }
        public Address? Address { get; set; }
    }
}
