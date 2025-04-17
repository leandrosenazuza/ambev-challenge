using Ambev.DeveloperEvaluation.Application.Sales.DTO;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{

    public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleValidator()
        {
            RuleFor(x => x.Customer)
                .NotEmpty().WithMessage("Customer is required.")
                .MaximumLength(255).WithMessage("Customer name cannot exceed 255 characters.");

            RuleFor(x => x.Branch)
                .NotEmpty().WithMessage("Branch is required.")
                .MaximumLength(255).WithMessage("Branch name cannot exceed 255 characters.");

        }
    }

    public class SaleItemDTOValidator : AbstractValidator<SaleDTO>
    {
        public SaleItemDTOValidator()
        {
            RuleForEach(x => x.Items).ChildRules(items =>
            {
                items.RuleFor(i => i.ProductId)
                    .GreaterThan(0).WithMessage("Product ID must be greater than 0.");

                items.RuleFor(i => i.Quantity)
                    .GreaterThan(0).WithMessage("Quantity must be greater than 0.")
                    .LessThanOrEqualTo(20).WithMessage("Quantity cannot exceed 20.")
                    .Must(q => q < 4 || q >= 4).WithMessage("Quantity must be at least 4 to apply discounts.");

                items.RuleFor(i => i.UnitPrice)
                    .GreaterThan(0).WithMessage("Unit price must be greater than 0.");
            });
        }
    }
}

