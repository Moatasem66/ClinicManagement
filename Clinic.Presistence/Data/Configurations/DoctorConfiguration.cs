using Clinic.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.Infrastructure.Data.Configurations;

/// <summary>
/// Configures the EF Core for Doctor  entity.
/// Each Doctor can has many schedule 
/// relation ship with Schedule entity one - many relation ship
/// when delete doctor delete all records in schedule for this doctor 
/// </summary>
public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.Property(d => d.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(d => d.PhoneNumber)
               .IsRequired()
               .HasMaxLength(20);

        builder.Property(d => d.Specification)
               .IsRequired()
               .HasMaxLength(100);

        builder.HasMany(d => d.Schedules)
               .WithOne(s => s.Doctor)
               .HasForeignKey(s => s.DoctorId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
