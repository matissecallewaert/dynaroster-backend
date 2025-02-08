using Core.Enums;

namespace Core;

public class Manager : User
{
    public List<Worker> Workers { get; set; } = [];

    public Manager()
    {
        Role = UserRole.Manager;
    }
}