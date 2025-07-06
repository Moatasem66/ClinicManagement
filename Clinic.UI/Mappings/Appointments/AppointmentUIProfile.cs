using AutoMapper;
using Clinic.Application.Dtos.Appointment;
using Clinic.UI.Models.Appointments;
namespace Clinic.UI.Mappings.Appointments;
public class AppointmentUIProfile : Profile
{
    public AppointmentUIProfile()
    {
        CreateMap<AppointmentDto, AppointmentViewModel>();
        CreateMap<AppointmentDto, CreateUpdateAppointmentViewModel>();
        CreateMap<CreateUpdateAppointmentViewModel, CreateUpdateAppointmentDto>();
    }
}
