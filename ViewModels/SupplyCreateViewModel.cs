using System.ComponentModel.DataAnnotations;

namespace MedicalSupplies.Mvc.ViewModels;

public class SupplyCreateViewModel
{
    [Required(ErrorMessage = "Mã SKU không được để trống.")]
    [RegularExpression(@"^[A-Z0-String]{3,}-[A-Z0-String]{3,}-\d+$", ErrorMessage = "SKU phải đúng định dạng (Ví dụ: MED-MSK-01).")]
    public string Sku { get; set; } = "";

    [Required(ErrorMessage = "Tên vật tư y tế không được để trống.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Tên vật tư phải từ 3 đến 100 ký tự.")]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Vui lòng chọn nhóm phân loại vật tư.")]
    public string Category { get; set; } = "";

    [Required(ErrorMessage = "Tên nhà cung cấp không được để trống.")]
    public string Supplier { get; set; } = "";

    [Required(ErrorMessage = "Đơn giá không được để trống.")]
    [Range(1000, 500000000, ErrorMessage = "Đơn giá phải nằm trong khoảng từ 1,000đ đến 500,000,000đ.")]
    public decimal UnitPrice { get; set; }

    [Required(ErrorMessage = "Số lượng tồn kho ban đầu không được để trống.")]
    [Range(0, 100000, ErrorMessage = "Số lượng tồn kho ban đầu phải từ 0 đến 100,000.")]
    public int Quantity { get; set; }

    [Required(ErrorMessage = "Mức tồn tối thiểu không được để trống.")]
    [Range(1, 5000, ErrorMessage = "Mức tồn an toàn tối thiểu phải từ 1 đến 5,000.")]
    public int MinStock { get; set; }
}