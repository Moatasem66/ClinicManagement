using Clinic.Application.Dtos;
using Clinic.Application.Dtos.Clinic;

namespace Clinic.Application.Contracts.Clinics;

/// <summary>
/// Service interface for managing clinic records.
/// </summary>
public interface IClinicService
{
    /// <summary>
    /// Creates a new clinic.
    /// </summary>
    Task<Result<string>> CreateAsync(CreateUpdateClinicDto dto);

    /// <summary>
    /// Updates an existing clinic by ID.
    /// </summary>
    Task<Result<string>> UpdateAsync(int id, CreateUpdateClinicDto dto);

    /// <summary>
    /// Deletes a clinic by ID.
    /// </summary>
    Task<Result<string>> DeleteAsync(int id);

    /// <summary>
    /// Retrieves a clinic by ID.
    /// </summary>
    Task<Result<ClinicDto>> GetByIdAsync(int id);

    /// <summary>
    /// Retrieves a paginated list of clinics.
    /// </summary>
    Task<Result<List<ClinicDto>>> GetListAsync();
}
