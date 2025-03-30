using HeThongMoiGioiDoCu.Interfaces;
using HeThongMoiGioiDoCu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HeThongMoiGioiDoCu.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product> AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> ApprovedProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return null;

            product.Status = "Approved";
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<int?> DeleteProduct(int ID)
        {
            var delProduct = await _context.Products.FindAsync(ID);
            if (delProduct == null)
                return null;

            _context.Products.Remove(delProduct);
            await _context.SaveChangesAsync();
            return ID;
        }

        public async Task<PagedResult<Product>> GetListProduct(
            int? categoryId,
            int? userId,
            string status,
            string keyword,
            int pageNumber,
            int pageSize
        )
        {
            var query = _context
                .Products.Include(p => p.Category)
                .Include(p => p.User)
                .AsQueryable();
            if (categoryId != null)
            {
                query = query.Where(p => p.CategoryID == categoryId);
            }
            if (userId != null)
            {
                query = query.Where(p => p.UserID == userId);
            }

            keyword = keyword.ToLower();
            query = query.Where(p =>
                p.Status.ToLower().Contains(status.ToLower())
                && (
                    p.Title.ToLower().Contains(keyword)
                    || p.Category.CategoryName.ToLower().Contains(keyword)
                )
            );

            var totalItems = await query.CountAsync();
            var data = await query
                .OrderByDescending(p => p.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new PagedResult<Product>
            {
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = data,
            };
        }

        public async Task<List<Product>> GetListProductByCategory(int CategoryID)
        {
            return await _context
                .Products.Include(p => p.Category)
                .Include(p => p.User)
                .Where(p => p.CategoryID == CategoryID)
                .ToListAsync();
        }

        public async Task<List<Product>> GetListProductByUser(int UserID)
        {
            return await _context
                .Products.Include(p => p.Category)
                .Include(p => p.User)
                .Where(p => p.UserID == UserID)
                .ToListAsync();
        }

        public async Task<Product> GetProductByID(int ID)
        {
            return await _context
                .Products.Include(p => p.Category)
                .Include(p => p.User)
                .FirstAsync(p => p.ProductID == ID);
        }

        public async Task<Product?> UpdateProduct(
            int ID,
            string? Title,
            string? Description,
            decimal? Price,
            int? CategoryID,
            string? Condition,
            string? Images,
            string? Location
        )
        {
            var product = await _context.Products.FindAsync(ID);
            if (product == null)
                return null;
            product.Title = Title ?? product.Title;
            product.Description = Description ?? product.Description;
            product.Price = Price ?? product.Price;
            product.CategoryID = CategoryID ?? product.CategoryID;
            product.Condition = Condition ?? product.Condition;
            product.Images = Images ?? product.Images;
            product.Location = Location ?? product.Location;
            product.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
