using System.Collections.ObjectModel;

namespace Clinic.Infrastructure.Entities;
/// <summary>
/// Clinic entity to store the basic info about clinic
/// </summary>
public class Clinic
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public virtual List<Doctor> Doctors { get; set; } = new();
}
