using Core.Entities.Base;

namespace Core;

public class Availability: BaseEntity
{
    public Guid WorkerId { get; set; }
    public Worker Worker { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
}   