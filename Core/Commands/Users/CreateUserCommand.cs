using Core.Dtos;
using Core.Enums;
using MediatR;

namespace Core.Commands.users
{
    public class CreateUserCommand : IRequest<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public AddressDto? Address { get; set; }
        public bool IsEmployee { get; set; }
        public string? ManagerId { get; set; }
    }
}
