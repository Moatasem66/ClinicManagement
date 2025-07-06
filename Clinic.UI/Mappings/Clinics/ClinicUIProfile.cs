using AutoMapper;
using Clinic.Application.Dtos.Clinic;
using Clinic.UI.Models.Clinics;

namespace Clinic.UI.Mappings.Clinics;

public class ClinicUIProfile : Profile
{
    public ClinicUIProfile()
    {
        CreateMap<ClinicDto, ClinicViewModel>();
        CreateMap<ClinicDto, CreateUpdateClinicViewModel>();
        CreateMap<CreateUpdateClinicViewModel, CreateUpdateClinicDto>();
    }
}
