namespace WebApplication1.Validators;

using FluentValidation;
using WebApplication1.DTOs;

public class ProductCreateValidator : AbstractValidator<ProductCreateDto>
{
    public ProductCreateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nazwa produktu jest wymagana!")
            .MaximumLength(150);

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Cena musi być wyższa niż zero.");

        RuleFor(x => x.CategoryName)
            .NotEmpty().WithMessage("Nazwa kategorii jest wymagana!")
            .MaximumLength(150);

        RuleFor(x => x.StockQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Ilość nie może być ujemna");
    }
}