using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> AddProductAsync(ProductCreateDto dto);
        Task <Product?> GetProductByIdAsync(int id);

    }

}
