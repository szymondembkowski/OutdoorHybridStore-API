using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductCreateDto> CreateAsync(ProductCreateDto dto)
        {
            var category = await _productRepository.GetCategoryByNameAsync(dto.CategoryName);

            if (category == null)
            {
                throw new KeyNotFoundException($"Category name: '{dto.CategoryName}' not found");
            }

            var product = dto.Adapt<Product>();
            product.CategoryId = category.Id;

            await _productRepository.AddAsync(product);
            return product.Adapt<ProductCreateDto>();
        }

        public async Task<IEnumerable<ProductReadDto>> GetAllAsync()
        {
            var products = await _productRepository.GetProductsAsync();
            return products.Adapt<IEnumerable<ProductReadDto>>();
        }
    }
}