using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopAPI.Application.Repositories.ProductRepo;
using ShopAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ShopAPI.Application.Features.ProductImageFiles.Commands.DeleteProductImage
{
    public class DeleteProductImageCommandHandler : IRequestHandler<DeleteProductImageCommandRequest, DeleteProductImageCommandResponse>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IProductWriteRepository _productWriteRepository;

        public DeleteProductImageCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        public async Task<DeleteProductImageCommandResponse> Handle(DeleteProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            Product? product = await _productReadRepository.Table
            .Include(x => x.ProductImageFiles)
               .Where(x => x.Id == Guid.Parse(request.Id)).FirstOrDefaultAsync();

            ProductImageFile? imageFile = product?.ProductImageFiles?.FirstOrDefault(x => x.Id == Guid.Parse(request.ImageId));
            product?.ProductImageFiles.Remove(imageFile);
            await _productWriteRepository.SaveAsync();
           
            return new();
            
        }
    }
}
