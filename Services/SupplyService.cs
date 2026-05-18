using MedicalSupplies.Mvc.Models;
using MedicalSupplies.Mvc.ViewModels;

namespace MedicalSupplies.Mvc.Services;

public class SupplyService
{
    private readonly List<Supply> _supplies = new()
    {
        new Supply { Id = 1, Sku = "MED-MSK-01", Name = "Khẩu trang y tế N95", Category = "Bảo hộ cá nhân", Supplier = "3M Medical", UnitPrice = 15000, Quantity = 500, MinStock = 100, LastUpdatedAt = DateTime.Now },
        new Supply { Id = 2, Sku = "MED-GLV-02", Name = "Găng tay y tế Nitrile", Category = "Bảo hộ cá nhân", Supplier = "VGlove", UnitPrice = 85000, Quantity = 20, MinStock = 50, LastUpdatedAt = DateTime.Now },
        new Supply { Id = 3, Sku = "MED-SYR-03", Name = "Bơm tiêm 5ml", Category = "Vật tư tiêu hao", Supplier = "Vinahankook", UnitPrice = 2000, Quantity = 0, MinStock = 200, LastUpdatedAt = DateTime.Now },
        new Supply { Id = 4, Sku = "MED-BND-04", Name = "Băng gác cuộn", Category = "Sơ cứu", Supplier = "Bảo Thạch", UnitPrice = 12000, Quantity = 300, MinStock = 50, LastUpdatedAt = DateTime.Now }
    };

    public List<Supply> GetAll() => _supplies;

    public Supply? GetById(int id) => _supplies.FirstOrDefault(s => s.Id == id);

    public SupplyStatsViewModel GetStats()
    {
        return new SupplyStatsViewModel
        {
            TotalSupplies = _supplies.Count,
            TotalQuantity = _supplies.Sum(s => s.Quantity),
            TotalInventoryValue = _supplies.Sum(s => s.UnitPrice * s.Quantity),
            OutOfStockCount = _supplies.Count(s => s.Quantity <= 0),
            NeedReorderCount = _supplies.Count(s => s.Quantity > 0 && s.Quantity <= s.MinStock)
        };
    }
}