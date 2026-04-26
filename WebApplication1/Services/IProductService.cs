using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IProductService
    {
        Task<ProductCreateDto> CreateAsync(ProductCreateDto dto);
        Task<IEnumerable<ProductReadDto>> GetAllAsync();
    }
}
