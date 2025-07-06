using System.Collections.ObjectModel;

namespace Clinic.Infrastructure.Entities;
/// <summary>
/// Doctor entity has relation ship with clinic 
/// Each doctor must be assigned to exactly one clinic. 
/// Mandatory relationship
/// </summary>
public class Doctor
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Specification { get; set; } = string.Empty;
    public int ClinicId { get; set; }
    public virtual Clinic Clinic { get; set; } 
    public virtual List<Schedule> Schedules { get; set; } 
    public virtual List<Appointment> Appointments { get; set; } 
}
