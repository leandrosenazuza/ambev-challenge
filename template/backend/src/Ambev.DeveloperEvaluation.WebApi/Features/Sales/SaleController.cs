using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DTO;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Common.Pagination;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.Query;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

[ApiController]
[Route("api/sale")]
public class SaleController : BaseController
{
    private readonly IMediator _mediator;
    private readonly ILogger<SaleController> _logger;

    public SaleController(IMediator mediator, IMapper mapper, ILogger<SaleController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSale(
      [FromBody] SaleDTO saleDto,
      CancellationToken cancellationToken)
    {
        var command = new CreateSaleCommand
        {
            SaleNumber = saleDto.SaleNumber,
            SaleDate = saleDto.SaleDate,
            Customer = saleDto.Customer,
            Branch = saleDto.Branch,
            Items = saleDto.Items,
            IsCancelled = saleDto.IsCancelled
        };

        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{saleNumber:guid}")]
    public async Task<ActionResult<SaleDTO>> GetSaleBySaleNumber(Guid saleNumber)
    {
        var query = new GetSaleBySaleNumberQuery(saleNumber);
        var result = await _mediator.Send(query);
        return result;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllSales(PaginationParameters parameters, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllSaleQuery(parameters), cancellationToken);
        return Ok(result);
    }

    [HttpPut("{saleNumber:guid}")]
    public async Task<IActionResult> UpdateSale([FromRoute] Guid saleNumber, [FromBody] SaleDTO saleDto, CancellationToken cancellationToken)
    {
        var command = new UpdateSaleCommand
        {
            SaleNumber = saleNumber,
            SaleDate = saleDto.SaleDate,
            Customer = saleDto.Customer,
            Branch = saleDto.Branch,
            Items = saleDto.Items,
            IsCancelled = saleDto.IsCancelled
        };
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{saleNumber:guid}")]
    public async Task<IActionResult> DeleteSale([FromRoute] Guid saleNumber, CancellationToken cancellationToken)
    {
        var command = new DeleteSaleCommand(saleNumber);
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(new { Message = "Sale deleted successfully", Data = result });
    }
}