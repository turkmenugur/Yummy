using FluentValidation;
using Yummy.WebApi.Entities;

namespace Yummy.WebApi.ValidationRules;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        //RuleFor(x => x.ProductName).NotEmpty().WithMessage("Ürün adı boş olamaz.");
        //RuleFor(x => x.ProductName).MinimumLength(2).WithMessage("En az 2 karakter olmalıdır.");
        
        RuleFor(p => p.ProductName)
            .NotEmpty().WithMessage("Ürün adı boş olamaz.")
            .Length(2, 50).WithMessage("Ürün adı 2 ile 50 karakter arasında olmalıdır.");

        RuleFor(p => p.Price)
            .GreaterThan(0).WithMessage("Ürün fiyatı sıfırdan büyük olmalıdır.");

        

        RuleFor(p => p.ProductDescription)
            .NotEmpty().MaximumLength(200).WithMessage("Açıklama en fazla 200 karakter olabilir.");
    }
}