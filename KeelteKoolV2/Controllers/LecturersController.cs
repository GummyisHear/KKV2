using KeelteKoolV2.ApplicationServices.Services;
using KeelteKoolV2.Core.ServiceInterface;
using KeelteKoolV2.Data;
using Microsoft.AspNetCore.Mvc;

namespace KeelteKoolV2.Controllers;

public class LecturersController : Controller
{
    private readonly KeelteKoolV2Context _context;
    private readonly ILecturerServices _lecturerServices;
    private readonly IFileServices _fileServices;

    public LecturersController(KeelteKoolV2Context context, 
        ILecturerServices lecturerServices, 
        IFileServices fileServices)
    {
        _context = context;
        _lecturerServices = lecturerServices;
        _fileServices = fileServices;
    }

    public IActionResult Index()
    {
        return View();
    }
}
