using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Clinic.UI.Models.Doctors;

public class CreateUpdateDoctorViewModel
{
    [Required(ErrorMessage = "Doctor name is required.")]
    [StringLength(100, ErrorMessage = "Doctor name must be less than 100 characters.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone number is required.")]
    [RegularExpression(@"^01[0125][0-9]{8}$", ErrorMessage = "Please enter a valid Egyptian phone number.")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Specification is required.")]
    [StringLength(100, ErrorMessage = "Specification must be less than 100 characters.")]
    public string Specification { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "Please select a clinic.")]
    public int ClinicId { get; set; }

    public List<SelectListItem> Clinics { get; set; } = new();
}
