using Clinic.Application.Contracts.Appointment;
using Clinic.Application.Contracts.Clinics;
using Clinic.Application.Contracts.Doctors;
using Clinic.Application.Contracts.Schedules;
using Clinic.Application.Mappings.Doctors;
using Clinic.Application.Services.Appointment;
using Clinic.Application.Services.Clinic;
using Clinic.Application.Services.Doctors;
using Clinic.Application.Services.Schedules;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Extensions;
public static class Extensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IClinicService, ClinicService>();
        services.AddScoped<IDoctorService, DoctorService>();
        services.AddScoped<IScheduleService, ScheduleService>();
        services.AddScoped<IAppointmentService, AppointmentService>();

        return services;
    }
}
