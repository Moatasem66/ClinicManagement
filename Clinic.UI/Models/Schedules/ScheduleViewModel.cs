namespace Clinic.UI.Models.Schedules;

public class ScheduleViewModel
{
    public int Id { get; set; }
    public int DoctorId { get; set; }
    public string DoctorName {  get; set; } = string.Empty;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}
