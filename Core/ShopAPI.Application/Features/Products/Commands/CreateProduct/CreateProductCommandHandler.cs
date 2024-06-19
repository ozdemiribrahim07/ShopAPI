using MediatR;
using ShopAPI.Application.Abstraction.Hubs;
using ShopAPI.Application.Repositories.ProductRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        readonly IProductWriteRepository _productWriteRepository;
        readonly IProductHubService _productHubService;


        public CreateProductCommandHandler(IProductWriteRepository productWriteRepository, IProductHubService productHubService)
        {
            _productWriteRepository = productWriteRepository;
            _productHubService = productHubService;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _productWriteRepository.AddAsync(
                new()
                {
                    ProductName = request.ProductName,
                    ProductStock = request.ProductStock,
                    ProductPrice = request.ProductPrice
                });
            await _productWriteRepository.SaveAsync();

            await _productHubService.ProductAddedMessage("Product Added");

            return new();
        }
    }
}
