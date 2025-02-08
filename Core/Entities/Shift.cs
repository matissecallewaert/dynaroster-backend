using Core.Entities.Base;

namespace Core;

public class Shift: BaseEntity
{
    public string Description { get; set; } = string.Empty;
    public int NeededWorkers { get; set; }
    public DateTime ShiftDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }

    public List<ShiftAssignment> Assignments { get; set; } = [];
}