using AutoMapper;
using Clinic.Application.Dtos.Appointment;
using Clinic.Infrastructure.Entities;

namespace Clinic.Application.Mappings.Appointments;

/// <summary>
/// AutoMapper profile for mapping between Appointment entity
/// and appointment-related DTOs.
/// </summary>
public class AppointmentProfile : Profile
{
    public AppointmentProfile()
    {
        CreateMap<Appointment, AppointmentDto>()
            .ForMember(dest => dest.DoctorName , map => map.MapFrom(src => src.Doctor.Name));
        CreateMap<CreateUpdateAppointmentDto, Appointment>()
            .ForMember( dest => dest.Doctor , map => map.Ignore());
    }
}
