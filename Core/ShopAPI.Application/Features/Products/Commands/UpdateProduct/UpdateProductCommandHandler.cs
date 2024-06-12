using MediatR;
using ShopAPI.Application.Repositories.ProductRepo;
using ShopAPI.Domain.Entities;
using ShopAPI.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IProductWriteRepository _productWriteRepository;

        public UpdateProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            Product product = await _productReadRepository.GetByIdAsync(request.Id);
            product.ProductName = request.ProductName;
            product.ProductStock = request.ProductStock;
            product.ProductPrice = request.ProductPrice;
            await _productWriteRepository.UpdateAsync(product);
            await _productWriteRepository.SaveAsync();
            return new();
        }
    }
}
