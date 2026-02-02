using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Services;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;

        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    db.Database.Migrate();

    if (!db.Products.Any())
    {
        var outdoorCategory = new Category { Name = "Outdoor" };

        var footwearSpecs = JsonSerializer.SerializeToDocument(new
        {
            Size = 44,
            Material = "Skóra"
        });

        var tentSpecs = JsonSerializer.SerializeToDocument(new
        {
            Capacity = 3,
            IsWaterproof = true
        });

        db.Products.AddRange(
            new Product
            {
                Name = "Buty Trekkingowe Garmont",
                Price = 599.99m,
                Specifications = footwearSpecs,
                Category = outdoorCategory
            },
            new Product
            {
                Name = "Namiot Fjord Nansen",
                Price = 850.00m,
                Specifications = tentSpecs,
                Category = outdoorCategory
            }
        );
        db.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();