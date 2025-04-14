
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

public class GetProductValidator : AbstractValidator<GetProductCommand>
{
    public GetProductValidator()
    {
        RuleFor(q => q.Id).GreaterThan(0)
        .WithMessage("The product id must be greater than zero.");
    }
}