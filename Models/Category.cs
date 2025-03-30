using MediaBrowser.Model.ProcessRun.Metrics;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HeThongMoiGioiDoCu.Models
{
    public class Category
    {
        public int CategoryID { get; set; }

        [Required]
        [MaxLength(100)]
        public string CategoryName { get; set; }

        public DateTime? CreatedAt { get; set; }
 
        [JsonIgnore]
        public ICollection<Product> Products { get; set; }
        public Category()
        {
            CreatedAt ??= DateTime.Now;
        }
    }
}
