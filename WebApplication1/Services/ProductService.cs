using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductService> _logger;

        public ProductService(ApplicationDbContext context, ILogger<ProductService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }

        public async Task<ProductCreateDto> AddProductAsync(ProductCreateDto dto)
        {
            _logger.LogInformation("Rozpoczynam dodawanie produktu: {ProductName} do kategorii: {CategoryName}", dto.Name, dto.CategoryName);

            try
            {
                var category = await _context.Categories
                    .FirstOrDefaultAsync(c => c.Name.ToLower() == dto.CategoryName.ToLower())
                    ?? new Category { Name = dto.CategoryName };

                var product = dto.Adapt<Product>();
                product.Category = category;

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Produkt {ProductName} został zapisany z ID: {ProductId}", product.Name, product.Id);

                return product.Adapt<ProductCreateDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Błąd podczas zapisu produktu {ProductName} do bazy danych", dto.Name);

                throw;
            }
        }

        public async Task<ProductCreateDto> GetProductByIdAsync(int id)
        {
            var product = await _context.Products
                    .Include(p => p.Category)
                    .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                throw new KeyNotFoundException($"Product with ID {id} not found.");

            return product.Adapt<ProductCreateDto>();
        }
    }
}