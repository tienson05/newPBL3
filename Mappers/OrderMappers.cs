using HeThongMoiGioiDoCu.Dtos.Order;
using HeThongMoiGioiDoCu.Dtos.OrderDetail;
using HeThongMoiGioiDoCu.Models;

namespace HeThongMoiGioiDoCu.Mappers
{
    public static class OrderMappers
    {
        public static Order OrderDtoToOrder(this OrderDto dto)
        {
            return new Order
            {
                BuyerId = dto.BuyerID,
                VendorID = dto.VendorID,
                TotalPrice = dto.TotalPrice,
                CreatedAt = DateTime.UtcNow,
                OrderDetails = dto.OrderDetails?.Select(od => new OrderDetail
                {
                    ProductID = od.ProductID,
                    Price = od.Price
                }).ToList()
            };
        }
    }
}
