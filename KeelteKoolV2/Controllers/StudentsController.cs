using KeelteKoolV2.Core.Domain;
using KeelteKoolV2.Core.DTO;
using KeelteKoolV2.Core.ServiceInterface;
using KeelteKoolV2.Data;
using KeelteKoolV2.Models.Students;
using Microsoft.AspNetCore.Mvc;

namespace KeelteKoolV2.Controllers;

public class StudentsController : Controller
{
    private readonly KeelteKoolV2Context _context;
    private readonly IStudentServices _studentServices;

    public StudentsController(KeelteKoolV2Context context, IStudentServices studentServices)
    {
        _context = context;
        _studentServices = studentServices;
    }

    public IActionResult Index()
    {
        var result = _context.Students
            .OrderBy(x => x.LastName)
            .ThenBy(x => x.FirstName)
            .Take(20)
            .Select(x => new StudentViewModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName
            })
            .ToList();
        return View(result);
    }

    [HttpGet]
    public IActionResult Create()
    {
        var vm = new StudentCreateUpdateViewModel();
        return View("CreateUpdate", vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(StudentCreateUpdateViewModel vm)
    {
        if (vm == null)
        {
            return RedirectToAction("Error", "Home");
        }

        if (!ModelState.IsValid)
        {
            return View("CreateUpdate", vm);
        }

        var dto = new StudentDTO
        {
            FirstName = vm.FirstName,
            LastName = vm.LastName,
            Grades = ParseGrades(vm.Grades)
        };

        var result = await _studentServices.Create(dto);

        if (result == null)
        {
            return RedirectToAction("Error", "Home");
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        if (id == Guid.Empty)
        {
            return NotFound();
        }

        var student = await _studentServices.DetailAsync(id);

        if (student == null)
        {
            return NotFound();
        }

        ViewData["ViewType"] = "details";

        return View("DetailsDelete", ToViewModel(student));
    }

    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(StudentCreateUpdateViewModel vm)
    {
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        return RedirectToAction(nameof(Index));
    }

    private static List<string> ParseGrades(string grades)
    {
        if (string.IsNullOrWhiteSpace(grades))
        {
            return [];
        }

        return grades
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .ToList();
    }

    private static StudentViewModel ToViewModel(Student student)
    {
        return new StudentViewModel
        {
            Id = student.Id,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Grades = string.Join(", ", student.Grades ?? []),
            CreatedAt = student.CreatedAt,
            ModifiedAt = student.ModifiedAt
        };
    }
}