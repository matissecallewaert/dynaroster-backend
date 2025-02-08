using Core.Entities.Base;

namespace Core;

public class WorkerSkill: BaseEntity
{
    public Guid WorkerId { get; set; }
    public Worker Worker { get; set; }
    public string SkillName { get; set; } = string.Empty;
    public int ProficiencyLevel { get; set; } // 1-10 Scale
}