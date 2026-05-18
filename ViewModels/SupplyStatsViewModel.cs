namespace MedicalSupplies.Mvc.ViewModels;

public class SupplyStatsViewModel
{
    public int TotalSupplies { get; set; }
    public int TotalQuantity { get; set; }
    public decimal TotalInventoryValue { get; set; }
    public int OutOfStockCount { get; set; }
    public int NeedReorderCount { get; set; }
    
    // Các thuộc tính phục vụ phần chi tiết thống kê 
    public int NormalStockCount { get; set; }
    public decimal MaxInventoryValue { get; set; }
    public decimal AvgInventoryValue { get; set; }
    public double AvgQuantity { get; set; }

    public string TotalInventoryValueText => $"{TotalInventoryValue:N0} VND";
    public string MaxInventoryValueText => $"{MaxInventoryValue:N0} VND";
    public string AvgInventoryValueText => $"{AvgInventoryValue:N0} VND";
    public string AvgQuantityText => $"{AvgQuantity:N2}";
}