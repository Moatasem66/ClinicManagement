using AutoMapper;
using Clinic.Application.Dtos.Doctor;
using Clinic.UI.Models.Doctors;

namespace Clinic.UI.Mappings.Doctors;

public class DoctorUIProfile : Profile
{
    public DoctorUIProfile()
    {
        CreateMap<DoctorDto, DoctorViewModel>();
        CreateMap<DoctorDto, CreateUpdateDoctorViewModel>();
        CreateMap<CreateUpdateDoctorViewModel, CreateUpdateDoctorDto>();
    }
}
