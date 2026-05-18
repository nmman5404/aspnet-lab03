using MedicalSupplies.Mvc.Models;
using MedicalSupplies.Mvc.ViewModels;

namespace MedicalSupplies.Mvc.Services;

public class SupplyService
{
    private readonly List<Supply> _supplies = new()
    {
        new Supply { Id = 1, Sku = "MED-MSK-01", Name = "Khẩu trang y tế", Category = "Bảo hộ cá nhân", Supplier = "3M Medical", UnitPrice = 15000, Quantity = 500, MinStock = 100, LastUpdatedAt = DateTime.Now },
        new Supply { Id = 2, Sku = "MED-GLV-02", Name = "Găng tay y tế", Category = "Bảo hộ cá nhân", Supplier = "VGlove", UnitPrice = 85000, Quantity = 20, MinStock = 50, LastUpdatedAt = DateTime.Now },
        new Supply { Id = 3, Sku = "MED-SYR-03", Name = "Bơm tiêm", Category = "Vật tư tiêu hao", Supplier = "Vinahankook", UnitPrice = 2000, Quantity = 0, MinStock = 200, LastUpdatedAt = DateTime.Now },
        new Supply { Id = 4, Sku = "MED-BND-04", Name = "Băng gác cuộn", Category = "Sơ cứu", Supplier = "Bảo Thạch", UnitPrice = 12000, Quantity = 300, MinStock = 50, LastUpdatedAt = DateTime.Now }
    };

    public List<Supply> GetAll() => _supplies;

    public Supply? GetById(int id) => _supplies.FirstOrDefault(s => s.Id == id);

    public SupplyStatsViewModel GetStats()
{
    int totalSupplies = _supplies.Count;
    int totalQuantity = _supplies.Sum(s => s.Quantity);
    decimal totalValue = _supplies.Sum(s => s.UnitPrice * s.Quantity);
    var inventoryValues = _supplies.Select(s => s.UnitPrice * s.Quantity).ToList();

    return new SupplyStatsViewModel
    {
        TotalSupplies = totalSupplies,
        TotalQuantity = totalQuantity,
        TotalInventoryValue = totalValue,
        OutOfStockCount = _supplies.Count(s => s.Quantity <= 0),
        NeedReorderCount = _supplies.Count(s => s.Quantity > 0 && s.Quantity <= s.MinStock),
        NormalStockCount = _supplies.Count(s => s.Quantity > s.MinStock),
        MaxInventoryValue = inventoryValues.Any() ? inventoryValues.Max() : 0,
        AvgInventoryValue = totalSupplies > 0 ? totalValue / totalSupplies : 0,
        AvgQuantity = totalSupplies > 0 ? (double)totalQuantity / totalSupplies : 0
    };
}
}