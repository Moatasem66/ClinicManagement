using AutoMapper;
using Clinic.Application.Contracts.Doctors;
using Clinic.Application.Dtos;
using Clinic.Application.Dtos.Doctor;
using Clinic.Infrastructure.Data.Context;
using Clinic.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
namespace Clinic.Application.Services.Doctors;
/// <summary>
/// Application service for managing doctors
/// </summary>
public class DoctorService : IDoctorService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public DoctorService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    /// <inheritdoc/>
    public async Task<Result<string>> CreateAsync(CreateUpdateDoctorDto dto)
    {
        try
        {
            var clinicExists = await _context.Clinics.AsNoTracking().AnyAsync(c => c.Id == dto.ClinicId);
            if (!clinicExists)
                return Result<string>.Failure("Clinic does not exist");

            var entity = _mapper.Map<Doctor>(dto);
            entity.ClinicId = dto.ClinicId;
            await _context.Doctors.AddAsync(entity);
            await _context.SaveChangesAsync();

            return Result<string>.Success("Doctor created successfully");
        }
        catch (Exception ex)
        {
            return Result<string>.Failure($"Failed to create doctor: {ex.Message}");
        }
    }
    /// <inheritdoc/>
    public async Task<Result<string>> UpdateAsync(int id, CreateUpdateDoctorDto dto)
    {
        try
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
                return Result<string>.Failure("Doctor not found");

            var clinicExists = await _context.Clinics.AnyAsync(c => c.Id == dto.ClinicId);
            if (!clinicExists)
                return Result<string>.Failure("Target clinic not found");

            _mapper.Map(dto, doctor);
            await _context.SaveChangesAsync();

            return Result<string>.Success("Doctor updated successfully");
        }
        catch (Exception ex)
        {
            return Result<string>.Failure($"Failed to update doctor: {ex.Message}");
        }
    }    
    /// <inheritdoc/>
    public async Task<Result<string>> DeleteAsync(int id)
    {
        try
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
                return Result<string>.Failure("Doctor not found");

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();

            return Result<string>.Success("Doctor deleted successfully");
        }
        catch (Exception ex)
        {
            return Result<string>.Failure($"Failed to delete doctor: {ex.Message}");
        }
    }   
    /// <inheritdoc/>
    public async Task<Result<DoctorDto>> GetByIdAsync(int id)
    {
        try
        {
            var doctor = await _context.Doctors.Include( d => d.Clinic)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);

            if (doctor == null)
                return Result<DoctorDto>.Failure("Doctor not found");

            var result = _mapper.Map<DoctorDto>(doctor);
            return Result<DoctorDto>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<DoctorDto>.Failure($"Failed to retrieve doctor: {ex.Message}");
        }
    }
    /// <inheritdoc/>
    public async Task<Result<List<DoctorDto>>> GetListAsync()
    {
        try
        {
            var doctors = await _context.Doctors.Include(c => c.Clinic)
                .AsNoTracking()
                .ToListAsync();

            var result = _mapper.Map<List<DoctorDto>>(doctors);
            return Result<List<DoctorDto>>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<List<DoctorDto>>.Failure($"Failed to retrieve doctor list: {ex.Message}");
        }
    }
}
