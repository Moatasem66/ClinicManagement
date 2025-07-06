namespace Clinic.Infrastructure.Entities;
/// <summary>
/// Schedule entity to create sedule for each doctor
/// </summary>
public class Schedule
{
    public int Id { get; set; }
    public int DoctorId { get; set; }
    public DateOnly StartDate { get; set; } 
    public DateOnly EndDate { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public virtual Doctor Doctor { get; set; } 
}
