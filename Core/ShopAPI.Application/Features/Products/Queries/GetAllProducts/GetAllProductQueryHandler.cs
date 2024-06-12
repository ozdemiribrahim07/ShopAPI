using MediatR;
using ShopAPI.Application.Repositories.FileRepo;
using ShopAPI.Application.Repositories.ProductRepo;
using ShopAPI.Application.RequestParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Application.Features.Products.Queries.GetAllProducts
{
   
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductsQueryRequest, GetAllProductsQueryResponse>
    {

        readonly IProductReadRepository _productReadRepository;

        public GetAllProductQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public Task<GetAllProductsQueryResponse> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {

            var total = _productReadRepository.GetAll().Count();
            var products = _productReadRepository.GetAll().Skip(request.Page * request.Size).Take(request.Size).Select(selector => new
            {
                selector.Id,
                selector.ProductName,
                selector.ProductPrice,
                selector.ProductStock,
                selector.CreatedDate,
                selector.UpdatedDate,
            }).ToList();

            var response = new GetAllProductsQueryResponse
            {
                Total = total,
                Products = products
            };

            return Task.FromResult(response);
        }
    }
}
