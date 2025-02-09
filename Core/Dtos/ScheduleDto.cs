namespace Core.Dtos;

public class ScheduleDto
{
    public string Name { get; set; } = string.Empty;
    public List<WorkerDto> Workers { get; set; } = [];
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

public class WorkerDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public byte[]? ProfilePicture { get; set; }
}