using Core.Enums;

namespace Core.Entities;

public class Manager : User
{
    public List<Worker> Workers { get; set; } = [];

    public Manager()
    {
        Role = UserRole.Manager;
    }
}