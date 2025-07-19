using System.ComponentModel.DataAnnotations;

namespace Clinic.UI.Models.Clinics;

public class CreateUpdateClinicViewModel
{
    [Required(ErrorMessage = "Clinic name is required")]
    [StringLength(100, ErrorMessage = "Name cannot be more than 100 characters")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Address is required")]
    [StringLength(maximumLength: 200, ErrorMessage = "Address cannot be more than 200 characters")]
    public string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone number is required")]
    [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Enter a valid Egyptian mobile number")]
    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; } = string.Empty;
}
