using KeelteKoolV2.ApplicationServices.Services;
using KeelteKoolV2.Core.DTO;
using KeelteKoolV2.Core.ServiceInterface;
using KeelteKoolV2.Data;
using KeelteKoolV2.Models.Lecturers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KeelteKoolV2.Controllers;

public class LecturersController : Controller
{
    private readonly KeelteKoolV2Context _context;
    private readonly ILecturerServices _lecturerServices;
    private readonly IFileServices _fileServices;

    public LecturersController(
        KeelteKoolV2Context context,
        ILecturerServices lecturerServices,
        IFileServices fileServices)
    {
        _context = context;
        _lecturerServices = lecturerServices;
        _fileServices = fileServices;
    }

    public async Task<IActionResult> Index()
    {
        var lecturers = await _context.Lecturers
            .Select(x => new LecturerViewModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Qualifications = x.Qualifications,
                UserID = x.UserID
            })
            .ToListAsync();

        return View(lecturers);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View("CreateUpdate", new LecturerCreateUpdateViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create(LecturerCreateUpdateViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            return View("CreateUpdate", vm);
        }

        var dto = new LecturerDTO
        {
            FirstName = vm.FirstName,
            LastName = vm.LastName,
            Qualifications = vm.Qualifications,
            UserID = vm.UserID
        };

        await _lecturerServices.Create(dto);

        return RedirectToAction(nameof(Index));
    }
}