using HeThongMoiGioiDoCu.Dtos.OrderDetail;

namespace HeThongMoiGioiDoCu.Dtos.Order
{
    public class OrderDto
    {
        public int BuyerID { get; set; }
        public int VendorID { get; set; }
        public decimal TotalPrice { get; set; }
        public List<CreateOrderDetailDto> OrderDetails { get; set; }
    }
}
