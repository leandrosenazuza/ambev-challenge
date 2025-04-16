using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
    {
        public CreateSaleRequestValidator()
        {

            RuleFor(x => x.Customer)
        .NotEmpty().WithMessage("Customer is required.")
        .MaximumLength(255).WithMessage("Customer must not exceed 255 characters.");

            RuleFor(x => x.Branch)
                .NotEmpty().WithMessage("Branch is required.")
                .MaximumLength(255).WithMessage("Branch must not exceed 255 characters.");

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("At least one sale item is required.")
                .Must(items => items != null && items.Count > 0).WithMessage("At least one sale item is required.");

            RuleForEach(x => x.Items).SetValidator(new CreateSaleItemRequestValidator());
        }
    }

    public class CreateSaleItemRequestValidator : AbstractValidator<CreateSaleItemRequest>
    {
        public CreateSaleItemRequestValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("ProductId must be a positive integer.");

            RuleFor(x => x.Quantity)
                .InclusiveBetween(1, 20).WithMessage("Quantity must be between 1 and 20.");

            RuleFor(x => x.UnitPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Unit price must be greater than or equal to zero.");
        }
    }
}
