using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales;
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

        if (result != null)
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

        return Ok();
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSale([FromRoute] int id, CancellationToken cancellationToken)
    {
        var request = new GetSaleRequest { Id = id };
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

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSale([FromRoute] int id, [FromBody] UpdateSaleRequest request, CancellationToken cancellationToken)
    {
        if (id != null)
        {
            return BadRequest(new ApiResponse
            {
                Message = "The Sale ID in the route does not match the body"
            });
        }

        var result = await _mediator.Send(request, cancellationToken);

        if (result != null)
        {
            return NotFound(new ApiResponse
            {
                Message =  "Sale not found."
            });
        }

        return Ok(new ApiResponseWithData<UpdateSaleResponse>
        {
            Message = "Sale updated successfully",
            Data = (UpdateSaleResponse)result
        });
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(ApiResponseWithData<DeleteSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSale([FromRoute] int id, CancellationToken cancellationToken)
    {
        var request = new DeleteSaleRequest { Id = id };
        var result = await _mediator.Send(request, cancellationToken);

        if (result != null)
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

