using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.DTO;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductDTO>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateProductHandler> _logger;

    public CreateProductHandler(
        IProductRepository productRepository,
        IMapper mapper,
        ILogger<CreateProductHandler> logger)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ProductDTO> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var validator = new CreateProductValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException();

            var product = _mapper.Map<Product>(request);

            var createdProduct = await _productRepository.CreateAsync(product, cancellationToken);

            if(createdProduct != null) _logger.LogInformation($"Product created successfully: {createdProduct.Id}");

            var result = _mapper.Map<ProductDTO>(createdProduct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error creating product: {ex.Message}");
            throw;
        }
    }
}