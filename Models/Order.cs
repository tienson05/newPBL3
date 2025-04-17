namespace HeThongMoiGioiDoCu.Models
{
    public class Order
    {
        public  int OrderID {  get; set; }
        public int BuyerId { get; set; }
        public int VendorID { get; set; }
        public Decimal TotalPrice { get; set; }
        public string Status { get; set; } // Pending, Completed, Cancelled
        public DateTime? CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }

        public User Buyer { get; set; }
        public User Vendor { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        public Order()
        {
            CreatedAt ??= DateTime.Now;
            CompletedAt ??= DateTime.Now;
            Status ??= "Pending";
        }
    }
}
