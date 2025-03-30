using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MediaBrowser.Model.ProcessRun.Metrics;

namespace HeThongMoiGioiDoCu.Models
{
    public class PagedResult<T>
    {
        public int TotalItems { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);
        public List<T> Data { get; set; }
    }
}
