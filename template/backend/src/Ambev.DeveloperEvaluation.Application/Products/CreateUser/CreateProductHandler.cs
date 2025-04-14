using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.ORM.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public CreateProductHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    Task<CreateProductResult> IRequestHandler<CreateProductCommand, CreateProductResult>.Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /* public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
     {
         var validator = new CreateProductCommandValidator();
         var validationResult = await validator.ValidateAsync(command, cancellationToken);

         if (!validationResult.IsValid)
             throw new ValidationException(validationResult.Errors);

         var existingProduct = await _productRepository.GetByCategoryAsync(command.Category, cancellationToken);
         var duplicateProduct = existingProduct.FirstOrDefault(p => p.Title.Trim().ToLower() == command.Title.Trim().ToLower());

         if (duplicateProduct != null)
             throw new InvalidOperationException($"Product with title '{command.Title}' already exists in category '{command.Category}'");

         var product = _mapper.Map<Product>(command);

         product.RatingRate = 0m;
         product.RatingCount = 0;

         var createdProduct = await _productRepository.CreateAsync(product, cancellationToken);

         var result = _mapper.Map<CreateProductResult>(createdProduct);
         return result;
     }*/
}
