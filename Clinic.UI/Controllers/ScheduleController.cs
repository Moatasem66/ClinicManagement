using AutoMapper;
using Clinic.Application.Contracts.Doctors;
using Clinic.Application.Contracts.Schedules;
using Clinic.Application.Dtos.Schedule;
using Clinic.UI.Models.Schedules;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clinic.UI.Controllers;

public class ScheduleController : Controller
{
    private readonly IScheduleService _scheduleService;
    private readonly IDoctorService _doctorService;
    private readonly IMapper _mapper;

    public ScheduleController(IScheduleService scheduleService, IDoctorService doctorService, IMapper mapper)
    {
        _scheduleService = scheduleService;
        _doctorService = doctorService;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var schedules = await _scheduleService.GetListAsync();
        var viewModels = _mapper.Map<List<ScheduleViewModel>>(schedules.Data);
        return View(viewModels);
    }

    public async Task<IActionResult> Create()
    {
        var doctors = await _doctorService.GetListAsync();
        var doctorList = doctors.Data.Select(d => new SelectListItem
        {
            Value = d.Id.ToString(),
            Text = d.Name
        }).ToList();

        return View(new CreateUpdateScheduleViewModel
        {
            Doctors = doctorList
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUpdateScheduleViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Doctors = await GetDoctorListAsync();
            return View(model);
        }

        var dto = _mapper.Map<CreateUpdateScheduleDto>(model);
        var result = await _scheduleService.CreateAsync(dto);

        if (result.IsSuccess)
        {
            ViewData["SuccessMessage"] = "Schedule created successfully.";
            return RedirectToAction(nameof(Index));
        }

        ViewBag.ErrorMessage = result.Error;
        model.Doctors = await GetDoctorListAsync();
        return View(model);
    }

    public async Task<IActionResult> Update(int id)
    {
        var schedule = await _scheduleService.GetByIdAsync(id);
        if (schedule == null || schedule.Data == null)
            return NotFound();

        var viewModel = _mapper.Map<CreateUpdateScheduleViewModel>(schedule.Data);
        viewModel.Doctors = await GetDoctorListAsync();

        return View("Update", viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, CreateUpdateScheduleViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Doctors = await GetDoctorListAsync();
            return View(model);
        }

        var dto = _mapper.Map<CreateUpdateScheduleDto>(model);
        var result = await _scheduleService.UpdateAsync(id, dto);

        if (result.IsSuccess)
        {
            ViewData["SuccessMessage"] = "Schedule updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        ViewBag.ErrorMessage = result.Error;
        model.Doctors = await GetDoctorListAsync();
        return View("Update", model);
    }

    public async Task<IActionResult> Details(int id)
    {
        var schedule = await _scheduleService.GetByIdAsync(id);
        if (schedule == null || schedule.Data == null)
            return NotFound();

        var viewModel = _mapper.Map<ScheduleViewModel>(schedule.Data);
        return View(viewModel);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var schedule = await _scheduleService.GetByIdAsync(id);
        if (schedule == null || schedule.Data == null)
            return NotFound();

        var viewModel = _mapper.Map<ScheduleViewModel>(schedule.Data);
        return View(viewModel);
    }

    [HttpPost, ActionName("DeleteConfirmed")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var result = await _scheduleService.DeleteAsync(id);

        if (result.IsSuccess)
        {
            ViewData["SuccessMessage"] = "Schedule deleted successfully.";
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
