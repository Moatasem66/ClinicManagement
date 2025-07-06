using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Clinic.UI.Models.Schedules;

public class CreateUpdateScheduleViewModel
{
    [Range(1, int.MaxValue, ErrorMessage = "Please select a doctor.")]
    public int DoctorId { get; set; }

    [Required(ErrorMessage = "Start date is required.")]
    public DateOnly StartDate { get; set; }

    [Required(ErrorMessage = "End date is required.")]
    public DateOnly EndDate { get; set; }

    [Required(ErrorMessage = "Start time is required.")]
    public TimeOnly StartTime { get; set; }

    [Required(ErrorMessage = "End time is required.")]
    public TimeOnly EndTime { get; set; }

    public List<SelectListItem> Doctors { get; set; } = new();
}
