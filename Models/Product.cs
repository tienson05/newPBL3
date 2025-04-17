using MediaBrowser.Model.ProcessRun.Metrics;

namespace HeThongMoiGioiDoCu.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int? CategoryID { get; set; }
        public string Condition { get; set; }
        public string Images { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Optional: Navigation properties
        public Users? User { get; set; }
        public Category? Category { get; set; }

        public Product()
        {
            CreatedAt ??= DateTime.Now;
            Status ??= "Pending";
            UpdatedAt ??= DateTime.Now;
        }
    }
}
