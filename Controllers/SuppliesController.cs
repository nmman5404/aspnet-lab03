using MedicalSupplies.Mvc.Models;
using MedicalSupplies.Mvc.Services;
using MedicalSupplies.Mvc.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MedicalSupplies.Mvc.Controllers;

public class SuppliesController : Controller
{
    private readonly SupplyService _supplyService;

    public SuppliesController(SupplyService supplyService)
    {
        _supplyService = supplyService;
    }

    // 1. Action trả về View danh sách
    public IActionResult Index()
    {
        var supplies = _supplyService.GetAll().Select(s => new SupplyListItemViewModel
        {
            Id = s.Id,
            Sku = s.Sku,
            Name = s.Name,
            Category = s.Category,
            UnitPrice = s.UnitPrice,
            Quantity = s.Quantity,
            MinStock = s.MinStock
        }).ToList();

        return View(supplies);
    }

    // 2. Action trả về View chi tiết hoặc NotFound
    public IActionResult Detail(int id)
    {
        var supply = _supplyService.GetById(id);
        if (supply == null)
        {
            return NotFound($"Không tìm thấy vật tư y tế có mã id = {id}");
        }

        var viewModel = new SupplyDetailViewModel
        {
            Id = supply.Id, Sku = supply.Sku, Name = supply.Name,
            Category = supply.Category, Supplier = supply.Supplier,
            UnitPrice = supply.UnitPrice, Quantity = supply.Quantity,
            MinStock = supply.MinStock, LastUpdatedAt = supply.LastUpdatedAt
        };

        return View(viewModel);
    }

    // 3. Action trả về View thống kê
    public IActionResult Stats()
    {
        var stats = _supplyService.GetStats();
        return View(stats);
    }

    // 4. Action trả về JSON
    public IActionResult SupplyJson()
    {
        return Json(_supplyService.GetAll());
    }

    // 5. Action trả về chuỗi Text
    public IActionResult Welcome()
    {
        return Content("Hệ thống quản lý vật tư y tế - Mini Medical Supplies Catalog");
    }

    // 6. Action chuyển hướng
    public IActionResult GoToList()
    {
        return RedirectToAction(nameof(Index));
    }

    // 7. Action cố tình trả về 404
    public IActionResult Force404()
    {
        return NotFound("Đường dẫn này không khả dụng.");
    }
}