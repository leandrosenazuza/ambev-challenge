using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{

    public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleCommandValidator()
        {
            RuleFor(x => x.Customer)
                .NotEmpty().WithMessage("Customer is required.")
                .MaximumLength(255).WithMessage("Customer name cannot exceed 255 characters.");

            RuleFor(x => x.Branch)
                .NotEmpty().WithMessage("Branch is required.")
                .MaximumLength(255).WithMessage("Branch name cannot exceed 255 characters.");

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("At least one item is required.")
                .Must(items => items.Count <= 20).WithMessage("Cannot have more than 20 items in a sale.")
                .ForEach(item =>
                {
                    item.SetValidator(new SaleItemDtoValidator());
                });
        }
    }

    public class SaleItemDtoValidator : AbstractValidator<SaleItemDto>
    {
        public SaleItemDtoValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("Product ID must be greater than 0.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.")
                .LessThanOrEqualTo(20).WithMessage("Quantity cannot exceed 20.")
                .Must(q => q < 4 || q >= 4).WithMessage("Quantity must be at least 4 to apply discounts.");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0).WithMessage("Unit price must be greater than 0.");
        }
    }
}

