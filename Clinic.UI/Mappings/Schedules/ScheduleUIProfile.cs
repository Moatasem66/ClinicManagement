using AutoMapper;
using Clinic.Application.Dtos.Doctor;
using Clinic.Application.Dtos.Schedule;
using Clinic.UI.Models.Doctors;
using Clinic.UI.Models.Schedules;

namespace Clinic.UI.Mappings.Schedules;

public class ScheduleUIProfile : Profile
{
    public ScheduleUIProfile()
    {
        CreateMap<ScheduleDto, ScheduleViewModel>();
        CreateMap<ScheduleDto, CreateUpdateScheduleViewModel>();
        CreateMap<CreateUpdateScheduleViewModel, CreateUpdateScheduleDto>();
    }
}
