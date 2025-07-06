using AutoMapper;
using Clinic.Application.Dtos.Schedule;
using Clinic.Infrastructure.Entities;

namespace Clinic.Application.Mappings.Schedules;
/// <summary>
/// AutoMapper profile for mapping between Schedule entity
/// and schedule-related DTOs.
/// </summary>
public class ScheduleProfile : Profile
{
    public ScheduleProfile()
    {
        CreateMap<Schedule, ScheduleDto>()
            .ForMember(x => x.DoctorName, map => map.MapFrom(src => src.Doctor.Name));
        ;
        CreateMap<CreateUpdateScheduleDto, Schedule>()
            .ForMember( dest => dest.Doctor , map => map.Ignore());
    }
}
