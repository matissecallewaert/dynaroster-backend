using Core.Entities.Base;

namespace Core.Entities;

public class Schedule: BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public List<Shift> Shifts { get; set; } = [];
    public List<User> Workers { get; set; } = [];
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}