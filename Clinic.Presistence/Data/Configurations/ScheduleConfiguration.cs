using Clinic.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Clinic.Infrastructure.Data.Configurations;
/// <summary>
/// Configures the EF Core for Schedule entity.
/// </summary>
public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.Property(s => s.StartDate)
               .IsRequired();

        builder.Property(s => s.EndDate)
               .IsRequired();

        builder.Property(s => s.StartTime)
               .IsRequired();

        builder.Property(s => s.EndTime)
               .IsRequired();
    }
}
