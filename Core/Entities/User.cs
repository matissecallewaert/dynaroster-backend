using Core.Entities.Base;
using Core.Enums;

namespace Core.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public Address? Address { get; set; }
    public UserRole Role { get; set; } = UserRole.Worker;
    public byte[]? ProfilePicture { get; set; }
    public List<Schedule> Schedules { get; set; } = [];
}
