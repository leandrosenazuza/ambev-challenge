using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DTO;
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
        return CreatedAtAction(nameof(GetSaleBySaleNumber), new { saleNumber = result.SaleNumber }, result);
    }

    [HttpGet("{saleNumber:guid}")]
    public async Task<ActionResult<SaleDTO>> GetSaleBySaleNumber(Guid saleNumber)
    {
        var query = new GetSaleBySaleNumberQuery(saleNumber);
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFound(new { Message = "Sale not found" });
        }
        return result; 
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSales(PaginationParameters parameters, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllSaleQuery(parameters), cancellationToken);
        return Ok(result);
    }

    [HttpPut("{saleNumber:guid}")]
    public async Task<IActionResult> UpdateSale([FromRoute] Guid saleNumber, [FromBody] SaleDTO saleDto, CancellationToken cancellationToken)
    {
        if (saleNumber != saleDto.SaleNumber)
        {
            return BadRequest(new { Message = "The Sale identifier in the route does not match the request body." });
        }

        var command = new UpdateSaleCommand
        {
            // Map properties from SaleDTO to UpdateSaleCommand if necessary
            SaleNumber = saleDto.SaleNumber,
            SaleDate = saleDto.SaleDate,
            Customer = saleDto.Customer,
            TotalSaleAmount = saleDto.TotalSaleAmount,
            Branch = saleDto.Branch,
            Items = saleDto.Items,
            IsCancelled = saleDto.IsCancelled
        };

        var result = await _mediator.Send(command, cancellationToken);

        if (result == null)
        {
            return NotFound(new { Message = "Sale not found." });
        }

        return Ok(result); 
    }

    [HttpDelete("{saleNumber:guid}")]
    public async Task<IActionResult> DeleteSale([FromRoute] Guid saleNumber, CancellationToken cancellationToken)
    {
        var command = new DeleteSaleCommand { SaleNumber = saleNumber }; 
        var result = await _mediator.Send(command, cancellationToken);

        if (result == null)
        {
            return NotFound(new { Message = "Sale not found." });
        }

        return Ok(new { Message = "Sale deleted successfully", Data = result }); 
    }
}