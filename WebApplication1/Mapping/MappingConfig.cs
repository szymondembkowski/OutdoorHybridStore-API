using Mapster;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Mapping;

public static class MappingConfig
{
    public static void Configure()
    {
        TypeAdapterConfig<ProductCreateDto, Product>.NewConfig()
            .Map(dest => dest.Specifications, src =>
                src.Specifications.HasValue
                ? System.Text.Json.JsonDocument.Parse(src.Specifications.Value.GetRawText(), default)
                : null);

        TypeAdapterConfig<Product, ProductCreateDto>.NewConfig()
            .Map(dest => dest.Specifications, src =>
                src.Specifications != null ? src.Specifications.RootElement : (System.Text.Json.JsonElement?)null);
    }
}