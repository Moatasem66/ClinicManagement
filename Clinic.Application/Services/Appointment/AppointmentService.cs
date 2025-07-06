using AutoMapper;
using Clinic.Application.Contracts.Appointment;
using Clinic.Application.Dtos;
using Clinic.Application.Dtos.Appointment;
using Clinic.Infrastructure.Data.Context;
using Clinic.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Application.Services.Appointment;
/// <summary>
/// Application service for managing appointments
/// </summary>
public class AppointmentService : IAppointmentService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public AppointmentService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    /// <inheritdoc/>
    public async Task<Result<string>> CreateAsync(CreateUpdateAppointmentDto dto)
    {
        try
        {
            var doctorExists = await _context.Doctors.AnyAsync(d => d.Id == dto.DoctorId);
            if (!doctorExists)
                return Result<string>.Failure("Doctor does not exist");

            bool hasValidSchedule = await CheckDoctorSchedule(dto);
            if (!hasValidSchedule)
                return Result<string>.Failure("No schedule available for the doctor during the selected time.");

            bool hasOverlappingAppointment = await CheckOverlapAppointment(dto);
            if (hasOverlappingAppointment)
                return Result<string>.Failure("Doctor already has an appointment during the selected time.");
           
            var appointment = _mapper.Map<Infrastructure.Entities.Appointment>(dto);
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();

            return Result<string>.Success("Appointment created successfully");
        }
        catch (Exception ex)
        {
            return Result<string>.Failure($"Failed to create appointment: {ex.Message}");
        }
    }

    private async Task<bool> CheckOverlapAppointment(CreateUpdateAppointmentDto dto)
    {
        return await _context.Appointments.AnyAsync(a =>
            a.DoctorId == dto.DoctorId &&
            a.Date == dto.Date &&
            dto.StartTime < a.EndTime &&
            dto.EndTime > a.StartTime
        );
    }

    private async Task<bool> CheckDoctorSchedule(CreateUpdateAppointmentDto dto)
    {
        return await _context.Schedules.AnyAsync(s =>
            s.DoctorId == dto.DoctorId &&
            dto.Date >= s.StartDate &&
            dto.Date <= s.EndDate &&
            dto.StartTime >= s.StartTime &&
            dto.EndTime <= s.EndTime
        );
    }

    /// <inheritdoc/>
    public async Task<Result<string>> UpdateAsync(int id, CreateUpdateAppointmentDto dto)
    {
        try
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
                return Result<string>.Failure("Appointment not found");

            var doctorExists = await _context.Doctors.AnyAsync(d => d.Id == dto.DoctorId);
            if (!doctorExists)
                return Result<string>.Failure("Target doctor does not exist");

            _mapper.Map(dto, appointment);
            await _context.SaveChangesAsync();

            return Result<string>.Success("Appointment updated successfully");
        }
        catch (Exception ex)
        {
            return Result<string>.Failure($"Failed to update appointment: {ex.Message}");
        }
    }
    /// <inheritdoc/>
    public async Task<Result<string>> DeleteAsync(int id)
    {
        try
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
                return Result<string>.Failure("Appointment not found");

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return Result<string>.Success("Appointment deleted successfully");
        }
        catch (Exception ex)
        {
            return Result<string>.Failure($"Failed to delete appointment: {ex.Message}");
        }
    }
    /// <inheritdoc/>

    public async Task<Result<AppointmentDto>> GetByIdAsync(int id)
    {
        try
        {
            var appointment = await _context.Appointments.Include(d => d.Doctor)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);

            if (appointment == null)
                return Result<AppointmentDto>.Failure("Appointment not found");

            var dto = _mapper.Map<AppointmentDto>(appointment);
            return Result<AppointmentDto>.Success(dto);
        }
        catch (Exception ex)
        {
            return Result<AppointmentDto>.Failure($"Failed to retrieve appointment: {ex.Message}");
        }
    }
    /// <inheritdoc/>
    public async Task<Result<List<AppointmentDto>>> GetListAsync()
    {
        try
        {
            var appointments = await _context.Appointments.Include(d => d.Doctor)
                .AsNoTracking()
                .ToListAsync();
            var result = _mapper.Map<List<AppointmentDto>>(appointments);
            return Result<List<AppointmentDto>>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<List<AppointmentDto>>.Failure($"Failed to retrieve appointments: {ex.Message}");
        }
    }
}
