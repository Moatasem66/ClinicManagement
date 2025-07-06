using AutoMapper;
using Clinic.Application.Contracts.Clinics;
using Clinic.Application.Contracts.Doctors;
using Clinic.Application.Dtos.Doctor;
using Clinic.UI.Models.Doctors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clinic.UI.Controllers;

public class DoctorController : Controller
{
    private readonly IDoctorService _doctorService;
    private readonly IClinicService _clinicService;
    private readonly IMapper _mapper;

    public DoctorController(IDoctorService doctorService, IMapper mapper, IClinicService clinicService)
    {
        _doctorService = doctorService;
        _mapper = mapper;
        _clinicService = clinicService;
    }

    public async Task<IActionResult> Index()
    {
        var doctors = await _doctorService.GetListAsync();
        var viewModels = _mapper.Map<List<DoctorViewModel>>(doctors.Data);
        return View(viewModels);
    }

    public async Task<IActionResult> Create()
    {
        var clinics = await _clinicService.GetListAsync();
        var model = new CreateUpdateDoctorViewModel
        {
            Clinics = clinics.Data.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList()
        };
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateUpdateDoctorViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        model.Clinics = await GetClinicsSelectListAsync();
        var dto = _mapper.Map<CreateUpdateDoctorDto>(model);
        var result = await _doctorService.CreateAsync(dto);

        if (result.IsSuccess)
        {
            ViewData["SuccessMessage"] = "Doctor created successfully.";
            return RedirectToAction(nameof(Index));
        }

        ViewBag.ErrorMessage = result.Error;
        return View(model);
    }

    public async Task<IActionResult> Update(int id)
    {
        var doctor = await _doctorService.GetByIdAsync(id);
        if (doctor == null || doctor.Data == null) return NotFound();

        var viewModel = _mapper.Map<CreateUpdateDoctorViewModel>(doctor.Data);
        return View("Update", viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, CreateUpdateDoctorViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var dto = _mapper.Map<CreateUpdateDoctorDto>(model);
        var result = await _doctorService.UpdateAsync(id, dto);

        if (result.IsSuccess)
        {
            ViewData["SuccessMessage"] = "Doctor updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        ViewBag.ErrorMessage = result.Error;
        return View("Update", model);
    }

    public async Task<IActionResult> Details(int id)
    {
        var doctor = await _doctorService.GetByIdAsync(id);
        if (doctor == null || doctor.Data == null) return NotFound();

        var viewModel = _mapper.Map<DoctorViewModel>(doctor.Data);
        return View(viewModel);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var doctor = await _doctorService.GetByIdAsync(id);
        if (doctor == null || doctor.Data == null) return NotFound();

        var viewModel = _mapper.Map<DoctorViewModel>(doctor.Data);
        return View(viewModel);
    }

    [HttpPost]
    [ActionName("DeleteConfirmed")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var result = await _doctorService.DeleteAsync(id);

        if (result.IsSuccess)
        {
            ViewData["SuccessMessage"] = "Doctor deleted successfully.";
        }
        else
        {
            ViewData["ErrorMessage"] = result.Error;
        }

        return RedirectToAction(nameof(Index));
    }
    private async Task<List<SelectListItem>> GetClinicsSelectListAsync()
    {
        var clinicsResult = await _clinicService.GetListAsync(); 

        if (clinicsResult == null || clinicsResult.Data == null)
            return new List<SelectListItem>();

        return clinicsResult.Data.Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = c.Name
        }).ToList();
    }

}
