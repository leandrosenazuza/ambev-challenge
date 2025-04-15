using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

[ApiController]
[Route("api/sale")]
public class SaleController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<SaleController> _logger;

    public SaleController(IMediator mediator, IMapper mapper, ILogger<SaleController> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSale(
    [FromBody] CreateSaleRequest request,
    CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);

        if (result == null)
        {
            return BadRequest(new ApiResponse
            {
                Message = "Error creating sale."
            });
        }

        var response = new ApiResponseWithData<CreateSaleResponse>
        {
            Message = "Sale created successfully",
            Data = (CreateSaleResponse)result
        };

        return Ok(response);
    }

    [HttpGet("{saleNumber:guid}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSale([FromRoute] Guid saleNumber, CancellationToken cancellationToken)
    {
        var request = new GetSaleRequest { SaleNumber = saleNumber };
        var result = await _mediator.Send(request, cancellationToken);

        if (result == null)
        {
            return NotFound(new ApiResponse
            {
                Message = "Sale not found"
            });
        }

        return Ok(new ApiResponseWithData<GetSaleResponse>
        {
            Message = "Sale retrieved successfully",
            Data = (GetSaleResponse)result
        });
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseWithData<GetAllSalesResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllSales([FromQuery] GetAllSalesRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);

        return Ok(new ApiResponseWithData<GetAllSalesResponse>
        {
            Message = "Sales list retrieved successfully",
            Data = (GetAllSalesResponse)result
        });
    }

    [HttpPut("{saleNumber:guid}")]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSale([FromRoute] Guid saleNumber, [FromBody] UpdateSaleRequest request, CancellationToken cancellationToken)
    {
        // Assuming UpdateSaleRequest contains a SaleNumber property.
        if (saleNumber != request.SaleNumber)
        {
            return BadRequest(new ApiResponse
            {
                Message = "The Sale identifier in the route does not match the request body."
            });
        }

        var result = await _mediator.Send(request, cancellationToken);

        if (result == null)
        {
            return NotFound(new ApiResponse
            {
                Message = "Sale not found."
            });
        }

        return Ok(new ApiResponseWithData<UpdateSaleResponse>
        {
            Message = "Sale updated successfully",
            Data = (UpdateSaleResponse)result
        });
    }

    [HttpDelete("{saleNumber:guid}")]
    [ProducesResponseType(typeof(ApiResponseWithData<DeleteSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSale([FromRoute] Guid saleNumber, CancellationToken cancellationToken)
    {
        var request = new DeleteSaleRequest { SaleNumber = saleNumber };
        var result = await _mediator.Send(request, cancellationToken);

        if (result == null)
        {
            return NotFound(new ApiResponse
            {
                Message = "Sale not found."
            });
        }

        return Ok(new ApiResponseWithData<DeleteSaleResponse>
        {
            Message = "Sale deleted successfully",
            Data = (DeleteSaleResponse)result
        });
    }
}

