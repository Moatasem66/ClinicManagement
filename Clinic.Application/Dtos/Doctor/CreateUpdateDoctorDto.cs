using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Dtos.Doctor;
public class CreateUpdateDoctorDto
{
    public string Name { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Specification { get; set; } = string.Empty;
    public int ClinicId { get; set; }
}
