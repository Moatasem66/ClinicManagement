using AutoMapper;
using Clinic.Application.Contracts.Clinics;
using Clinic.Application.Dtos.Clinic;
using Clinic.UI.Models.Clinics;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.UI.Controllers;

public class ClinicController : Controller
{
    private readonly IClinicService _clinicService;
    private readonly IMapper _mapper;

    public ClinicController(IClinicService clinicService, IMapper mapper)
    {
        _clinicService = clinicService;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var clinics = await _clinicService.GetListAsync();
        var viewModels = _mapper.Map<List<ClinicViewModel>>(clinics.Data);
        return View(viewModels);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUpdateClinicViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var dto = _mapper.Map<CreateUpdateClinicDto>(model);
        var result = await _clinicService.CreateAsync(dto);

        if (result.IsSuccess)
        {
            ViewData["SuccessMessage"] =  "Clinic created successfully.";
            return RedirectToAction(nameof(Index));
        }

        ViewBag.ErrorMessage = result.Error;
        return View(model);
    }
    public async Task<IActionResult> Update(int id)
    {
        var clinic = await _clinicService.GetByIdAsync(id);
        if (clinic == null) return NotFound();

        var viewModel = _mapper.Map<CreateUpdateClinicViewModel>(clinic.Data);
        return View("Update" ,viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, CreateUpdateClinicViewModel model)
    {
        
        if (!ModelState.IsValid) 
            return View(model);

        var dto = _mapper.Map<CreateUpdateClinicDto>(model);
        var result = await _clinicService.UpdateAsync(id, dto);

        if (result.IsSuccess)
        {
            ViewData["SuccessMessage"] = "Clinic updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        ViewBag.ErrorMessage = result.Error;
        return View("Update",model);
    }
    public async Task<IActionResult> Details(int id)
    {
        var clinic = await _clinicService.GetByIdAsync(id);
        if (clinic == null) return NotFound();

        var viewModel = _mapper.Map<ClinicViewModel>(clinic.Data);
        return View(viewModel);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var clinic = await _clinicService.GetByIdAsync(id);
        if (clinic == null) return NotFound();

        var viewModel = _mapper.Map<ClinicViewModel>(clinic.Data);
        return View(viewModel);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var result = await _clinicService.DeleteAsync(id);

        if (result.IsSuccess)
        {
            ViewData["SuccessMessage"] = "Clinic deleted successfully.";
        }
        else
        {
            ViewData["ErrorMessage"] = result.Error;
        }

        return RedirectToAction(nameof(Index));
    }
}
