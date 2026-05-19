namespace MedicalSupplies.Mvc.ViewModels;

public class SupplySearchViewModel
{
    // Các tiêu chí tìm kiếm người dùng nhập vào Form (GET)
    public string? Keyword { get; set; }
    public string? Category { get; set; }
    public string? Supplier { get; set; } 
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }

    // Danh sách kết quả trả về sau khi lọc
    public List<SupplyListItemViewModel> Results { get; set; } = new();
}