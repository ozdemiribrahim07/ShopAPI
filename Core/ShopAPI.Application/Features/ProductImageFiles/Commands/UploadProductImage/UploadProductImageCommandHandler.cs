using MediatR;
using Microsoft.AspNetCore.Http;
using ShopAPI.Application.Abstraction.Storage;
using ShopAPI.Application.Repositories.ProductImageRepo;
using ShopAPI.Application.Repositories.ProductRepo;
using ShopAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Application.Features.ProductImageFiles.Commands.UploadProductImage
{
    public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest, UploadProductImageCommandResponse>
    {
        readonly IStorageService _storageService;
        readonly IProductReadRepository _productReadRepository;
        readonly IProductImageWriteRepository _productImageWriteRepository;

        public UploadProductImageCommandHandler(IStorageService storageService, IProductReadRepository productReadRepository, IProductImageWriteRepository productImageWriteRepository)
        {
            _storageService = storageService;
            _productReadRepository = productReadRepository;
            _productImageWriteRepository = productImageWriteRepository;
        }

        public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            string path = "images-deneme";
            List<(string fileName, string pathOrbucketName)> result = await _storageService.UploadAsync(path, request.Files);

            Product product = await _productReadRepository.GetByIdAsync(request.Id);

            await _productImageWriteRepository.AddRangeAsync(result.Select(x => new ProductImageFile()
            {
                FileName = x.fileName,
                Path = x.pathOrbucketName,
                Storage = _storageService.StorageName,
                Products = new List<Product>() { product }

            }).ToList());

            await _productImageWriteRepository.SaveAsync();
            return new();
        }
    }
}
