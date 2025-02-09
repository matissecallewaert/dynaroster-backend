using System.Security.Claims;

namespace Application.Checks;

public static class IsSameUser
{
    public static bool CheckIsSameUser(ClaimsPrincipal user, Guid id)
    {
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userRole = user.FindFirst(ClaimTypes.Role)?.Value;

        return userRole == "SuperAdmin" || userId == id.ToString();
    }
}