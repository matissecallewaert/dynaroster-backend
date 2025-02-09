using Core.Enums;

namespace Core.Entities;

public class Worker: User
{
    public List<WorkerSkill> Skills { get; set; } = [];
    public List<Availability> Availabilities { get; set; } = [];
    public List<WorkPreference> Preferences { get; set; } = [];
    public Guid? ManagerId { get; set; }
    public Manager? Manager { get; set; }

    public Worker()
    {
        Role = UserRole.Worker;
    }
}