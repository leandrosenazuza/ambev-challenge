using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using FluentValidation;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.DTO;
using Microsoft.AspNetCore.Http;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDTO>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProductCommandHandler> _logger;
        private readonly UpdateProductValidator _validator;

        public UpdateProductCommandHandler(
            IProductRepository productRepository,
            IMapper mapper,
            ILogger<UpdateProductCommandHandler> logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
            _validator = new UpdateProductValidator();
        }

        public async Task<ProductDTO> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                {
                    var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                    throw new ValidationException($"Invalid product data: {errors}");
                }

                var existingProduct = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingProduct == null)
                {
                    throw new BadHttpRequestException($"Product with ID {request.Id} not found");
                }

                if (!existingProduct.Title.Equals(request.Title, StringComparison.OrdinalIgnoreCase))
                {
                    var existingProducts = await _productRepository.GetAllAsync(
                        new WebApi.Common.Pagination.PaginationParameters
                        {
                            Name = request.Title,
                            Page = 1,
                            PageSize = 1
                        },
                        cancellationToken);

                    if (existingProducts.Items.Any(p => p.Title.Equals(request.Title, StringComparison.OrdinalIgnoreCase)))
                    {
                        throw new InvalidOperationException($"Product with title '{request.Title}' already exists");
                    }
                }

                var product = _mapper.Map<Product>(request);
                product.CreatedAt = existingProduct.CreatedAt;

                var updatedProduct = await _productRepository.UpdateAsync(product, cancellationToken);

                _logger.LogInformation("Product updated successfully: {ProductId}", updatedProduct.Id);

                return _mapper.Map<ProductDTO>(updatedProduct);
            }
            catch (BadHttpRequestException ex)
            {
                _logger.LogWarning("Product not found: {Message}", ex.Message);
                throw;
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning("Validation failed for update product request: {Message}", ex.Message);
                throw;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning("Business rule violation for update product request: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product {ProductId}", request.Id);
                throw new ApplicationException("An error occurred while updating the product", ex);
            }
        }
    }
}