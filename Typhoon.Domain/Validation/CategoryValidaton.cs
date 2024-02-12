using FluentValidation;
using Typhoon.Domain.Entities;

namespace Typhoon.Domain.Validation
{
    public class CategoryValidaton : AbstractValidator<Category>
    {
        public CategoryValidaton()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
