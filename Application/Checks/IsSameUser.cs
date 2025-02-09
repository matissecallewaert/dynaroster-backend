using System.Security.Claims;

namespace Application.Checks;

public class IsSameUser
{
    public static bool CheckIsSameUser(ClaimsPrincipal user, Guid id)
    {
        var userId = user.FindFirst("sub")?.Value;
        var userRole = user.FindFirst("role")?.Value;

        return userRole == "SuperAdmin" || userId == id.ToString();
    }
}