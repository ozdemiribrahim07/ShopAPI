using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShopAPI.Application.Repositories.ProductRepo;
using ShopAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Application.Features.ProductImageFiles.Queries.GetProductImage
{
    public class GetProductImageQueryHandler : IRequestHandler<GetProductImageQueryRequest, List<GetProductImageQueryResponse>>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IConfiguration _configuration;

        public GetProductImageQueryHandler(IProductReadRepository productReadRepository, IConfiguration configuration)
        {
            _productReadRepository = productReadRepository;
            _configuration = configuration;
        }

        public async Task<List<GetProductImageQueryResponse>> Handle(GetProductImageQueryRequest request, CancellationToken cancellationToken)
        {
            Product? product = await _productReadRepository.Table
                .Include(x => x.ProductImageFiles)
                .Where(x => x.Id == Guid.Parse(request.Id)).FirstOrDefaultAsync();


            return product?.ProductImageFiles.Select(x => new GetProductImageQueryResponse
            {
                FileName = x.FileName,
                Path = $"{_configuration["BaseStorageUrl"]}/{x.FileName}",
                Id = x.Id.ToString()
            }).ToList();
        }
    }
}
