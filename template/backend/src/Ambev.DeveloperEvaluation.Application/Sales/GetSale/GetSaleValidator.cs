using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{

    public class GetSaleValidator : AbstractValidator<GetSaleCommand>
    {
        public GetSaleValidator()
        {
            RuleFor(q => q.SaleNumber)
                .NotEmpty()
                .WithMessage("The sale number is required.")
                .Must(BeAValidGuid)
                .WithMessage("The sale number must be a valid GUID.");
        }

        private bool BeAValidGuid(Guid saleNumber)
        {
            return saleNumber != Guid.Empty;
        }
    }
}