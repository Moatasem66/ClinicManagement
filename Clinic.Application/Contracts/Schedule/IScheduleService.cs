using Clinic.Application.Dtos.Schedule;
using Clinic.Application.Dtos;

namespace Clinic.Application.Contracts.Schedules;

/// <summary>
/// Service interface for managing doctor schedules.
/// </summary>
public interface IScheduleService
{
    /// <summary>
    /// Creates a new schedule entry.
    /// </summary>
    Task<Result<string>> CreateAsync(CreateUpdateScheduleDto dto);

    /// <summary>
    /// Updates an existing schedule by ID.
    /// </summary>
    Task<Result<string>> UpdateAsync(int id, CreateUpdateScheduleDto dto);

    /// <summary>
    /// Deletes a schedule by ID.
    /// </summary>
    Task<Result<string>> DeleteAsync(int id);

    /// <summary>
    /// Retrieves a schedule by ID.
    /// </summary>
    Task<Result<ScheduleDto>> GetByIdAsync(int id);

    /// <summary>
    /// Retrieves a paginated list of schedules.
    /// </summary>
    Task<Result<List<ScheduleDto>>> GetListAsync();
}
