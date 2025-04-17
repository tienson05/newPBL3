using HeThongMoiGioiDoCu.Dtos.OrderDetail;

namespace HeThongMoiGioiDoCu.Dtos.Order
{
    public class OrderUpdateDto
    {
        public int OrderID { get; set; }
        public int BuyerId { get; set; }
        public int VendorID { get; set; }
        public Decimal TotalPrice{ get; set; }
        public string Status{ get; set; }
        public int OrderDetailID { get; set; }
        public int ProductID { get; set; }
        public int Price { get; set; }
    }
}
