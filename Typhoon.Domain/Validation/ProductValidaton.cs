using FluentValidation;
using Typhoon.Domain.Entities;

namespace Typhoon.Domain.Validation
{
    public class ProductValidaton : AbstractValidator<Product>
    {
        public ProductValidaton()
        {

            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty().MaximumLength(250);
            RuleFor(x => x.Brand).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.CategoryId).NotEmpty();
        }
    }
}
