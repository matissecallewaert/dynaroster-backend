using Core.Entities;
using MediatR;

namespace Core.Commands.users
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public Guid Id { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public Address? Address { get; set; }
    }
}
