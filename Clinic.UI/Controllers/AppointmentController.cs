using AutoMapper;
using Clinic.Application.Contracts.Appointment;
using Clinic.Application.Contracts.Doctors;
using Clinic.Application.Dtos.Appointment;
using Clinic.UI.Models.Appointments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clinic.UI.Controllers;

public class AppointmentController : Controller
{
    private readonly IAppointmentService _appointmentService;
    private readonly IDoctorService _doctorService;
    private readonly IMapper _mapper;

    public AppointmentController(
        IAppointmentService appointmentService,
        IDoctorService doctorService,
        IMapper mapper)
    {
        _appointmentService = appointmentService;
        _doctorService = doctorService;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var result = await _appointmentService.GetListAsync();
        var viewModels = _mapper.Map<List<AppointmentViewModel>>(result.Data);
        return View(viewModels);
    }

    public async Task<IActionResult> Create()
    {
        return View(new CreateUpdateAppointmentViewModel
        {
            Date = DateOnly.FromDateTime(DateTime.Now),
            StartTime = TimeOnly.FromDateTime(DateTime.Now),
            EndTime = TimeOnly.FromDateTime(DateTime.Now).AddMinutes(30) , 
            Doctors = await GetDoctorListAsync()
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUpdateAppointmentViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Doctors = await GetDoctorListAsync();
            return View(model);
        }

        var dto = _mapper.Map<CreateUpdateAppointmentDto>(model);
        var result = await _appointmentService.CreateAsync(dto);

        if (result.IsSuccess)
        {
            ViewData["SuccessMessage"] = "Appointment created successfully.";
            return RedirectToAction(nameof(Index));
        }

        ViewBag.ErrorMessage = result.Error;
        model.Doctors = await GetDoctorListAsync();
        return View(model);
    }
    public async Task<IActionResult> Details(int id)
    {
        var appointment = await _appointmentService.GetByIdAsync(id);
        if (appointment == null || appointment.Data == null)
            return NotFound();

        var viewModel = _mapper.Map<AppointmentViewModel>(appointment.Data);
        return View(viewModel);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var appointment = await _appointmentService.GetByIdAsync(id);
        if (appointment == null || appointment.Data == null)
            return NotFound();

        var viewModel = _mapper.Map<AppointmentViewModel>(appointment.Data);
        return View(viewModel);
    }

    [HttpPost, ActionName("DeleteConfirmed")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var result = await _appointmentService.DeleteAsync(id);

        if (result.IsSuccess)
        {
            ViewData["SuccessMessage"] = "Appointment deleted successfully.";
        }
        else
        {
            ViewData["ErrorMessage"] = result.Error;
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task<List<SelectListItem>> GetDoctorListAsync()
    {
        var doctors = await _doctorService.GetListAsync();
        return doctors.Data.Select(d => new SelectListItem
        {
            Value = d.Id.ToString(),
            Text = d.Name
        }).ToList();
    }
}
