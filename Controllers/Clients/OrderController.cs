using HeThongMoiGioiDoCu.Dtos.Order;
using HeThongMoiGioiDoCu.Interfaces;
using HeThongMoiGioiDoCu.Mappers;
using HeThongMoiGioiDoCu.Models;
using HeThongMoiGioiDoCu.Services;
using Microsoft.AspNetCore.Authorization;

//using MediaBrowser.Model.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HeThongMoiGioiDoCu.Controllers.Clients
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly AccountService _accountService;

        public OrderController(IOrderRepository orderRepository, AccountService accountService)
        {
            _orderRepository = orderRepository;
            _accountService = accountService;
        }

        [HttpGet("id")]
        public async Task<ActionResult<Order>> GetOrderByID(int id)
        {
            try
            {
                return await _orderRepository.GetOrderByID(id);
            }
            catch (Exception ex)
            {
                return NotFound("Order not found!");
            }
        }
        [HttpGet("userid")]
        public async Task<IActionResult> GetListOrderByUserID([FromQuery] int id, [FromQuery] int pageNumber=1, [FromQuery] int pageSize= 10)
        {
            var result = await _orderRepository.GetListOrderByUserID(id, pageNumber,pageSize);
            return Ok(result);
        }
        [Authorize]
        [HttpDelete("id")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var delID = await _orderRepository.DeleteOrder(id);
            if (delID != null) return Ok("Order delete success!");
            return NotFound("Order not found!");
        }
        //[Authorize]
        [HttpPost]
        public async Task<ActionResult<Order>> AddOrder([FromBody]OrderDto dto)
        {
            var order = dto.OrderDtoToOrder();
            _orderRepository.AddOrder(order);
            var savedOrder = await _orderRepository.GetDetailOrderByID(order.OrderID);
            //var returnOrder = savedOrder.Map
            return CreatedAtAction(nameof(GetOrderByID), new { id = order.OrderID }, order);
        }
        [HttpPut ("id")]
        public async Task<ActionResult<Order>> UpdateOrder(int id, [FromBody] OrderUpdateDto dto)
        {
            var UpdateOrder = await _orderRepository.UpdateOrder(id, dto.BuyerId, dto.VendorID, dto.TotalPrice, dto.Status, dto.OrderDetailID, dto.ProductID, dto.Price);
            if (UpdateOrder == null)
                return NotFound();
            return Ok(UpdateOrder);
        }

    }
}
