namespace Clinic.Infrastructure.Entities;
/// <summary>
/// Represents a scheduled appointment between a patient and medical provider.
/// </summary>
public class Appointment
{
    public int Id { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public int DoctorId { get; set; }
    public virtual Doctor Doctor { get; set; }
}