using KeelteKoolV2.Core.DTO;
using KeelteKoolV2.Data;
using KeelteKoolV2.Models.LanguageCourses;
using KeelteKoolV2.Models.Students;
using Microsoft.AspNetCore.Mvc;

namespace KeelteKoolV2.Controllers;

public class StudentsController : Controller
{
    private readonly KeelteKoolV2Context _context;

    public StudentsController(KeelteKoolV2Context context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        //var result = _context.LanguageCourses
        //    .Select(x => new LanguageCourseViewModel
        //    {
        //        Id = x.Id,
        //        Nimetus = x.Nimetus,
        //        Keel = x.Keel,
        //    }).Take(20).OrderBy(x => x.Keel);
        return View();
    }

    [HttpGet]
    public IActionResult Create()
    {
        //LanguageCourseCreateUpdateViewModel vm = new();
        //return View("CreateUpdate", vm);
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(StudentsViewModel vm)
    {
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Update(StudentsViewModel vm)
    {
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid? id)
    {
        return NotFound();
    }

    [HttpPost]
    [ActionName("DeleteConfirmed")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return NotFound();
    }
}
