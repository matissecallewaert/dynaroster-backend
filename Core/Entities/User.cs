using Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace Core;

public class User : IdentityUser
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime DateAdded { get; set; } = DateTime.UtcNow;
    public DateTime? DateUpdated { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public Address? Address { get; set; }
    public UserRole Role { get; set; } = UserRole.Worker;
    public bool IsActive { get; set; } = true;
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
}
