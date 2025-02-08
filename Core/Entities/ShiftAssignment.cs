using Core.Entities.Base;

namespace Core;

public class ShiftAssignment: BaseEntity
{
    public Guid ShiftId { get; set; }
    public Shift Shift { get; set; } = null!;
    public Guid WorkerId { get; set; }
    public Worker Worker { get; set; } = null!;
}