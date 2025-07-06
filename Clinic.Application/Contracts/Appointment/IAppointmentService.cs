using Clinic.Application.Dtos.Appointment;
using Clinic.Application.Dtos;

namespace Clinic.Application.Contracts.Appointment;

/// <summary>
/// Service interface for managing appointments.
/// </summary>
public interface IAppointmentService
{
    /// <summary>
    /// Creates a new appointment.
    /// </summary>
    Task<Result<string>> CreateAsync(CreateUpdateAppointmentDto dto);

    /// <summary>
    /// Updates an existing appointment.
    /// </summary>
    Task<Result<string>> UpdateAsync(int id, CreateUpdateAppointmentDto dto);

    /// <summary>
    /// Deletes an appointment by ID.
    /// </summary>
    Task<Result<string>> DeleteAsync(int id);

    /// <summary>
    /// Retrieves an appointment by ID.
    /// </summary>
    Task<Result<AppointmentDto>> GetByIdAsync(int id);

    /// <summary>
    /// Retrieves a paginated list of appointments.
    /// </summary>
    Task<Result<List<AppointmentDto>>> GetListAsync();
}
