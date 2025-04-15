using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleRequestValidator : AbstractValidator<GetSaleRequest>
    {
        public GetSaleRequestValidator()
        {
            RuleFor(x => x.SaleNumber)
                .NotEmpty().WithMessage("Sale number is required.");
        }
    }
}