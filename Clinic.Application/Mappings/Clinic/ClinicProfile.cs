using AutoMapper;
using Clinic.Application.Dtos.Clinic;
using Clinic.Infrastructure.Entities;

namespace Clinic.Application.Mappings.Clinics;
/// <summary>
/// AutoMapper profile for mapping between Clinic entity
/// and clinic-related DTOs.
/// </summary>
public class ClinicProfile : Profile
{
    public ClinicProfile()
    {
        CreateMap<Infrastructure.Entities.Clinic, ClinicDto>();
        CreateMap<CreateUpdateClinicDto, Infrastructure.Entities.Clinic>();
    }
}
