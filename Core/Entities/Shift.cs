using Core.Entities.Base;

namespace Core.Entities;

public class Shift: BaseEntity
{
    public string Description { get; set; } = string.Empty;
    public int NeededWorkers { get; set; }
    public DateTime ShiftDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public Guid ScheduleId { get; set; }
    public Schedule Schedule { get; set; }
    public List<ShiftAssignment> Assignments { get; set; } = [];
    public Address Location { get; set; }
    public Guid LocationId { get; set; }
}