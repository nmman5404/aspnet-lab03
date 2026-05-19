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

    // 8. CHỨC NĂNG TÌM KIẾM (HTTP GET)
    [HttpGet]
    public IActionResult Search(SupplySearchViewModel query)
    {
        var data = _supplyService.GetAll().AsQueryable();

        // Thực hiện lọc dữ liệu theo các tiêu chí nếu có nhập
        if (!string.IsNullOrEmpty(query.Keyword))
        {
            data = data.Where(s => s.Name.Contains(query.Keyword, StringComparison.OrdinalIgnoreCase) 
                                || s.Sku.Contains(query.Keyword, StringComparison.OrdinalIgnoreCase));
        }
        if (!string.IsNullOrEmpty(query.Category))
        {
            data = data.Where(s => s.Category == query.Category);
        }
        if (query.MinPrice.HasValue)
        {
            data = data.Where(s => s.UnitPrice >= query.MinPrice.Value);
        }
        if (query.MaxPrice.HasValue)
        {
            data = data.Where(s => s.UnitPrice <= query.MaxPrice.Value);
        }

        // Ánh xạ kết quả sang danh sách hiển thị
        query.Results = data.Select(s => new SupplyListItemViewModel
        {
            Id = s.Id, Sku = s.Sku, Name = s.Name, Category = s.Category,
            UnitPrice = s.UnitPrice, Quantity = s.Quantity, MinStock = s.MinStock
        }).ToList();

        return View(query);
    }

    // 9. CHỨC NĂNG THÊM MỚI - HIỂN THỊ FORM TRỐNG (HTTP GET)
    [HttpGet]
    public IActionResult Create()
    {
        return View(new SupplyCreateViewModel());
    }

    // 10. CHỨC NĂNG THÊM MỚI - TIẾP NHẬN DỮ LIỆU SUBMIT FORM (HTTP POST)
    [HttpPost]
    [ValidateAntiForgeryToken] // Phòng chống tấn công giả mạo yêu cầu chéo trang CSRF
    public IActionResult Create(SupplyCreateViewModel model)
    {
        // Kiểm tra xem dữ liệu gửi lên có vi phạm DataAnnotations quy định không
        if (!ModelState.IsValid)
        {
            // Nếu có lỗi, trả lại giao diện Form kèm các thông báo lỗi hiển thị cho User sửa
            return View(model);
        }

        // Nếu dữ liệu hợp lệ, ánh xạ từ ViewModel sang Model gốc
        var newSupply = new Supply
        {
            Sku = model.Sku,
            Name = model.Name,
            Category = model.Category,
            Supplier = model.Supplier,
            UnitPrice = model.UnitPrice,
            Quantity = model.Quantity,
            MinStock = model.MinStock
        };

        // Gọi tầng Service lưu vào bộ nhớ
        _supplyService.Add(newSupply);

        // Dùng TempData lưu thông báo thành công tạm thời (bị xóa ngay sau khi đọc 1 lần)
        TempData["SuccessMessage"] = $"Thêm mới vật tư y tế '{model.Name}' thành công!";

        // Áp dụng PRG Pattern: Điều hướng người dùng về trang danh sách, tránh lặp dữ liệu khi F5
        return RedirectToAction(nameof(Index));
    }
}