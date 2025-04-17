using HeThongMoiGioiDoCu.Dtos.Product;
using HeThongMoiGioiDoCu.Interfaces;
using HeThongMoiGioiDoCu.Models;
using HeThongMoiGioiDoCu.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HeThongMoiGioiDoCu.Controllers.Clients
{
    [Route("api/product")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly AccountService _accountService;

        public ProductsController(
            IProductRepository productRepository,
            AccountService accountService
        )
        {
            _productRepository = productRepository;
            _accountService = accountService;
        }

        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<ActionResult> GetProducts(
            [FromQuery] int? category_id = null,
            [FromQuery] int? user_id = null,
            [FromQuery] int page_number = 1,
            [FromQuery] int page_size = 10,
            [FromQuery] string? status = null,
            [FromQuery] string? keyword = null
        )
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            Console.Write("aaaaa" + userIdClaim);
            var result = await _productRepository.GetListProduct(
                category_id.HasValue ? category_id.Value : null,
                user_id.HasValue ? user_id.Value : null,
                status ?? "",
                keyword ?? "",
                page_number,
                page_size
            );
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductByID(int id)
        {
            try
            {
                return await _productRepository.GetProductByID(id);
            }
            catch (Exception e)
            {
                return NotFound("Product not found!");
            }
            ;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> AddProducts([FromBody] Product product)
        {
            var newProduct = await _productRepository.AddProduct(product);
            return CreatedAtAction(
                nameof(GetProductByID),
                new { id = newProduct.ProductID },
                newProduct
            );
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var delProID = await _productRepository.DeleteProduct(id);
            return delProID != null
                ? Ok("Product delete success!")
                : NotFound("Product Not Found!");
        }

        [Authorize(Roles="Admin")]
        [HttpPost("{id}/approved")]
        public async Task<ActionResult> ApprovedProduct(int id)
        {
            var product = await _productRepository.ApprovedProduct(id);
            return product != null
                ? Ok("Product approved success!")
                : NotFound("Product Not Found!");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody] UpdateProductDto product)
        {
            var updateProduct = await _productRepository.UpdateProduct(
                id,
                product.Title,
                product.Description,
                product.Price,
                product.CategoryID,
                product.Condition,
                product.Images,
                product.Location
            );
            return updateProduct != null ? Ok(updateProduct) : NotFound("Product Not Found!");
        }
    }
}
