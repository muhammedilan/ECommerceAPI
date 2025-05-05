using ECommerceAPI.Application.Features.Commands.Product.CreateProduct;
using FluentValidation;

namespace ECommerceAPI.Application.Validators.Products
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommandRequest>
    {
        public CreateProductCommandValidator()
        {
            var nameErrorMsg = "Lütfen ürün adını boş geçmeyiniz";
            var nameLengthErrorMsg = "Lütfen ürün adını 5 ile 150 karakter arasında giriniz";
            var stockErrorMsg = "Lütfen stok bilgisini boş geçmeyiniz";
            var priceErrorMsg = "Lütfen fiyat bilgisini boş geçmeyiniz";

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage(nameErrorMsg)
                .NotNull().WithMessage(nameErrorMsg)
                .MaximumLength(150).WithMessage(nameLengthErrorMsg)
                .MinimumLength(5).WithMessage(nameLengthErrorMsg);

            RuleFor(p => p.Stock)
                .NotEmpty().WithMessage(stockErrorMsg)
                .NotNull().WithMessage(stockErrorMsg)
                .Must(s => s >= 0).WithMessage("Stok bilgisi negatif olamaz!");

            RuleFor(p => p.Price)
                .NotEmpty().WithMessage(priceErrorMsg)
                .NotNull().WithMessage(priceErrorMsg)
                .Must(s => s >= 0).WithMessage("Fiyat bilgisi negatif olamaz!");
        }
    }
}
