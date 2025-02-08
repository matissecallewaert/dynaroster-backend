using Core.Entities.Base;

namespace Core;

public class WorkPreference: BaseEntity
{
    public Guid WorkerId { get; set; }
    public Worker Worker { get; set; }
    public string PreferredJob { get; set; } = string.Empty;
}