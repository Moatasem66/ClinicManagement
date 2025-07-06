using AutoMapper;
using Clinic.Application.Dtos.Doctor;
using Clinic.Infrastructure.Entities;
namespace Clinic.Application.Mappings.Doctors;
/// <summary>
/// AutoMapper profile for mapping between Doctor entity
/// and doctor-related DTOs.
/// </summary>
public class DoctorProfile : Profile
{
    public DoctorProfile()
    {
        CreateMap<Doctor, DoctorDto>()
            .ForMember(x => x.ClinicName , map => map.MapFrom(src => src.Clinic.Name));
        CreateMap<CreateUpdateDoctorDto, Doctor>()
            .ForMember(d => d.ClinicId  , map => map.MapFrom(src => src.ClinicId))
            .ForMember(dest => dest.Clinic, map => map.Ignore());
    }
}
