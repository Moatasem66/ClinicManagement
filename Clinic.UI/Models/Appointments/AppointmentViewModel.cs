namespace Clinic.UI.Models.Appointments;
public class AppointmentViewModel
{
    public int Id { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public int DoctorId { get; set; }
    public string DoctorName { get; set; } = string.Empty ;
}
