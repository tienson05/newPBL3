using HeThongMoiGioiDoCu.Models;

namespace HeThongMoiGioiDoCu.Dtos.Product
{
    public class UpdateProductDto
    {
        public int ProductID { get; set; }
        public int? UserID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? CategoryID { get; set; }
        public string? Condition { get; set; }
        public string? Images { get; set; }
        public string? Location { get; set; }
    }
}
