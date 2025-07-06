using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Clinic.UI.Models.Appointments;

public class CreateUpdateAppointmentViewModel
{
    [Required(ErrorMessage = "Patient name is required.")]
    [StringLength(100, ErrorMessage = "Patient name must be less than 100 characters.")]
    public string PatientName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone number is required.")]
    [RegularExpression(@"^01[0125][0-9]{8}$", ErrorMessage = "Phone number must be a valid .")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Date is required.")]
    public DateOnly Date { get; set; }

    [Required(ErrorMessage = "Start time is required.")]
    public TimeOnly StartTime { get; set; }

    [Required(ErrorMessage = "End time is required.")]
    [Compare(nameof(StartTime), ErrorMessage = "End time must be after start time.")]
    public TimeOnly EndTime { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Please select a doctor.")]
    public int DoctorId { get; set; }

    public List<SelectListItem> Doctors { get; set; } = new();
}
