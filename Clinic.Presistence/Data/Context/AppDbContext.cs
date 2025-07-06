using Clinic.Infrastructure.Data.Configurations;
using Clinic.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
namespace Clinic.Infrastructure.Data.Context;
/// <summary>
/// DbContext is the session between your app and Database 
/// </summary>
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /// <summary>
        /// EfCore will scan all files type of clinic configuration and apply the configuration from this files 
        /// </summary>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClinicConfiguration).Assembly);

        base.OnModelCreating(modelBuilder);
    }
    public DbSet<Entities.Clinic> Clinics { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
}
