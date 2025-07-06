using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Clinic.Application.Contracts.Clinics;
using Clinic.Application.Dtos;
using Clinic.Application.Dtos.Clinic;
using Clinic.Infrastructure.Data.Context;
namespace Clinic.Application.Services.Clinic;
/// <summary>
/// Application service for managing clinic
/// </summary>
public class ClinicService : IClinicService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ClinicService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    /// <inheritdoc/>
    public async Task<Result<string>> CreateAsync(CreateUpdateClinicDto dto)
    {
        try
        {
            var entity = _mapper.Map<Infrastructure.Entities.Clinic>(dto);
            _context.Clinics.Add(entity);
            await _context.SaveChangesAsync();

            return Result<string>.Success("Clinic created successfully");
        }
        catch (Exception ex)
        {
            return Result<string>.Failure($"Failed to create clinic: {ex.Message}");
        }
    }
    /// <inheritdoc/>
    public async Task<Result<List<ClinicDto>>> GetListAsync()
    {
        try
        {
            var pagedClinics = await _context.Clinics
                .AsNoTracking()
                .ToListAsync();

            var result = _mapper.Map<List<ClinicDto>>(pagedClinics);
            return Result<List<ClinicDto>>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<List<ClinicDto>>.Failure($"Failed to retrieve clinics: {ex.Message}");
        }
    }
    /// <inheritdoc/>
    public async Task<Result<ClinicDto>> GetByIdAsync(int id)
    {
        try
        {
            var entity = await _context.Clinics
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                return Result<ClinicDto>.Failure("Clinic not found");

            return Result<ClinicDto>.Success(_mapper.Map<ClinicDto>(entity));
        }
        catch (Exception ex)
        {
            return Result<ClinicDto>.Failure($"Failed to retrieve clinic: {ex.Message}");
        }
    }
    /// <inheritdoc/>
    public async Task<Result<string>> DeleteAsync(int id)
    {
        try
        {
            var clinic = await _context.Clinics.FindAsync(id);
            if (clinic == null)
                return Result<string>.Failure("Clinic not found");

            _context.Clinics.Remove(clinic);
            await _context.SaveChangesAsync();
            return Result<string>.Success("Clinic deleted successfully");
        }
        catch (Exception ex)
        {
            return Result<string>.Failure($"Failed to delete clinic: {ex.Message}");
        }
    }
    /// <inheritdoc/>
    public async Task<Result<string>> UpdateAsync(int id, CreateUpdateClinicDto dto)
    {
        try
        {
            var entity = await _context.Clinics.FindAsync(id);
            if (entity == null)
                return Result<string>.Failure("Clinic not found");
            _mapper.Map(dto, entity); 

            await _context.SaveChangesAsync();
            return Result<string>.Success("Clinic updated successfully");
        }
        catch (Exception ex)
        {
            return Result<string>.Failure($"Failed to update clinic: {ex.Message}");
        }
    }
}
