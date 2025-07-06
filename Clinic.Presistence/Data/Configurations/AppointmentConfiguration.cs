using Clinic.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Clinic.Infrastructure.Data.Configurations;
/// <summary>
/// Configures the EF Core for Appointment entity.
/// </summary>
public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.Property(a => a.PatientName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(a => a.PhoneNumber)
               .IsRequired()
               .HasMaxLength(20);

        builder.Property(a => a.Date)
               .IsRequired();

        builder.Property(a => a.StartTime)
               .IsRequired();

        builder.Property(a => a.EndTime)
               .IsRequired();

        builder.HasOne(a => a.Doctor)
               .WithMany(a => a.Appointments)
               .HasForeignKey(a => a.DoctorId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
