using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Common.Pagination;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.DTO;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products;
[ApiController]
[Route("api/products")]
public class ProductController : BaseController
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProductController> _logger;

    public ProductController(IMediator mediator, ILogger<ProductController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProductById(int id, CancellationToken cancellationToken)
    {
        var query = new GetProductByIdQuery(id);
        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductDTO productDto, CancellationToken cancellationToken)
    {
        var ratingCommand = new ProductRatingCommand(productDto.Rating.Rate, productDto.Rating.Count);
        var command = new CreateProductCommand(
            productDto.Id,
            productDto.Title,
            productDto.Price,
            productDto.Description,
            productDto.Category,
            productDto.Image,
            ratingCommand
        );

        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetProductById), new { id = result.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProduct(
        [FromRoute] int id,
        [FromBody] ProductDTO productDto,
        CancellationToken cancellationToken)
    {
        var ratingCommand = new ProductRatingCommand(productDto.Rating.Rate, productDto.Rating.Count);
        var command = new UpdateProductCommand(
            id,
            productDto.Title,
            productDto.Price,
            productDto.Description,
            productDto.Category,
            productDto.Image,
            ratingCommand
        );

        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] int id, CancellationToken cancellationToken)
    {
        var command = new DeleteProductCommand(id);
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResponse { Success = true, Message = "Product deleted successfully" });
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllProducts(
       [FromQuery] PaginationParameters parameters,
       CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllProductsQuery(parameters), cancellationToken);
        return Ok(result);
    }
}