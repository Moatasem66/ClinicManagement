using AutoMapper;
using Clinic.Application.Contracts.Schedules;
using Clinic.Application.Dtos;
using Clinic.Application.Dtos.Schedule;
using Clinic.Infrastructure.Data.Context;
using Clinic.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Application.Services.Schedules;
/// <summary>
/// Application service for managing doctor schedules
/// </summary>
public class ScheduleService : IScheduleService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ScheduleService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    /// <inheritdoc/>
    public async Task<Result<string>> CreateAsync(CreateUpdateScheduleDto dto)
    {
        try
        {
            var doctorExists = await _context.Doctors.AnyAsync(d => d.Id == dto.DoctorId);
            if (!doctorExists)
                return Result<string>.Failure("Doctor does not exist");

            bool hasOverlap = await ValidateOverlap(dto);

            if (hasOverlap)
            {
                return Result<string>.Failure("Schedule overlaps with an existing schedule for this doctor.");
            }
            var entity = _mapper.Map<Schedule>(dto);
            _context.Schedules.Add(entity);
            await _context.SaveChangesAsync();

            return Result<string>.Success("Schedule created successfully");
        }
        catch (Exception ex)
        {
            return Result<string>.Failure($"Failed to create schedule: {ex.Message}");
        }
    }

    private async Task<bool> ValidateOverlap(CreateUpdateScheduleDto dto)
    {
        return await _context.Schedules
            .AnyAsync(s =>
                s.DoctorId == dto.DoctorId &&
                (
                    (dto.StartDate <= s.EndDate && dto.EndDate >= s.StartDate)
                    &&
                    (dto.StartTime < s.EndTime && dto.EndTime > s.StartTime)
                )
            );
    }

    /// <inheritdoc/>
    public async Task<Result<string>> UpdateAsync(int id, CreateUpdateScheduleDto dto)
    {
        try
        {
            var entity = await _context.Schedules.FindAsync(id);
            if (entity == null)
                return Result<string>.Failure("Schedule not found");

            var doctorExists = await _context.Doctors.AnyAsync(d => d.Id == dto.DoctorId);
            if (!doctorExists)
                return Result<string>.Failure("Target doctor not found");

            bool hasOverlap = await ValidateOverLapforUpdate(id, dto);
            if (hasOverlap)
            {
                return Result<string>.Failure("Schedule overlaps with an existing schedule for this doctor.");
            }
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();

            return Result<string>.Success("Schedule updated successfully");
        }
        catch (Exception ex)
        {
            return Result<string>.Failure($"Failed to update schedule: {ex.Message}");
        }
    }

    private async Task<bool> ValidateOverLapforUpdate(int id, CreateUpdateScheduleDto dto)
    {
        return await _context.Schedules
     .AnyAsync(s =>
         s.Id != id &&
         s.DoctorId == dto.DoctorId &&
         (dto.StartDate <= s.EndDate && dto.EndDate >= s.StartDate) &&
         (dto.StartTime < s.EndTime && dto.EndTime > s.StartTime)
     );
    }

    /// <inheritdoc/>
    public async Task<Result<string>> DeleteAsync(int id)
    {
        try
        {
            var entity = await _context.Schedules.FindAsync(id);
            if (entity == null)
                return Result<string>.Failure("Schedule not found");

            _context.Schedules.Remove(entity);
            await _context.SaveChangesAsync();

            return Result<string>.Success("Schedule deleted successfully");
        }
        catch (Exception ex)
        {
            return Result<string>.Failure($"Failed to delete schedule: {ex.Message}");
        }
    }
    /// <inheritdoc/>
    public async Task<Result<ScheduleDto>> GetByIdAsync(int id)
    {
        try
        {
            var entity = await _context.Schedules.Include(d => d.Doctor)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);

            if (entity == null)
                return Result<ScheduleDto>.Failure("Schedule not found");

            var result = _mapper.Map<ScheduleDto>(entity);
            return Result<ScheduleDto>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<ScheduleDto>.Failure($"Failed to retrieve schedule: {ex.Message}");
        }
    }
    /// <inheritdoc/>
    public async Task<Result<List<ScheduleDto>>> GetListAsync()
    {
        try
        {
            var schedules = await _context.Schedules.Include(s => s.Doctor)
                .AsNoTracking()
                .ToListAsync();

            var result = _mapper.Map<List<ScheduleDto>>(schedules);
            return Result<List<ScheduleDto>>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<List<ScheduleDto>>.Failure($"Failed to retrieve schedule list: {ex.Message}");
        }
    }
}
