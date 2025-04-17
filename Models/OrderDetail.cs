using System.Text.Json.Serialization;

namespace HeThongMoiGioiDoCu.Models
{
    public class OrderDetail
    {
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public decimal Price { get; set; }

        [JsonIgnore]
        public Order Order { get; set; }
        public Product Product { get; set; }

    }
}
