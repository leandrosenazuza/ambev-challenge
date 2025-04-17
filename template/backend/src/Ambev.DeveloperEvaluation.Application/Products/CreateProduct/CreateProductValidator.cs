using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
      
            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price must be greater than or equal to 0");

            RuleFor(x => x.Description)
                .MaximumLength(1000)
                .When(x => !string.IsNullOrEmpty(x.Description))
                .WithMessage("Description must not exceed 1000 characters");

            RuleFor(x => x.Category)
                .MaximumLength(100)
                .When(x => !string.IsNullOrEmpty(x.Category))
                .WithMessage("Category must not exceed 100 characters");

            RuleFor(x => x.Image)
                .MaximumLength(500)
                .When(x => !string.IsNullOrEmpty(x.Image))
                .WithMessage("Image URL must not exceed 500 characters");

            RuleFor(x => x.Rating.Rate)
                .InclusiveBetween(0, 5)
                .WithMessage("Rating rate must be between 0 and 5");

            RuleFor(x => x.Rating.Count)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Rating count must be greater than or equal to 0");
        }
    }
}