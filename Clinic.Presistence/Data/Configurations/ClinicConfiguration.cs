using Microsoft.EntityFrameworkCore;
namespace Clinic.Infrastructure.Data.Configurations;
/// <summary>
/// Configures the EF Core for Clinic entity.
/// CLinis has many doctors 
/// relationship with doctor entity one - many  
/// </summary>
public class ClinicConfiguration : IEntityTypeConfiguration<Entities.Clinic>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.Clinic> builder)
    {
        builder.Property(c => c.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(c => c.Address)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(c => c.PhoneNumber)
               .IsRequired()
               .HasMaxLength(20);

        builder.HasMany(c => c.Doctors)
               .WithOne(d => d.Clinic)
               .HasForeignKey(d => d.ClinicId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
