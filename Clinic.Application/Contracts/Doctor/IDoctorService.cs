using Clinic.Application.Dtos.Doctor;
using Clinic.Application.Dtos;

namespace Clinic.Application.Contracts.Doctors;

/// <summary>
/// Service interface for managing doctor records.
/// </summary>
public interface IDoctorService
{
    /// <summary>
    /// Creates a new doctor.
    /// </summary>
    Task<Result<string>> CreateAsync(CreateUpdateDoctorDto dto);

    /// <summary>
    /// Updates an existing doctor by ID.
    /// </summary>
    Task<Result<string>> UpdateAsync(int id, CreateUpdateDoctorDto dto);

    /// <summary>
    /// Deletes a doctor by ID.
    /// </summary>
    Task<Result<string>> DeleteAsync(int id);

    /// <summary>
    /// Retrieves a doctor by ID.
    /// </summary>
    Task<Result<DoctorDto>> GetByIdAsync(int id);

    /// <summary>
    /// Retrieves a paginated list of doctors.
    /// </summary>
    Task<Result<List<DoctorDto>>> GetListAsync();
}
