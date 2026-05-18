using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MedicalSupplies.Mvc.Models;

namespace MedicalSupplies.Mvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    // Action này bắt buộc phải có để render ra file Views/Home/Index.cshtml
    public IActionResult Index()
    {
        return View();
    }

    // Action xử lý trang Privacy 
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}