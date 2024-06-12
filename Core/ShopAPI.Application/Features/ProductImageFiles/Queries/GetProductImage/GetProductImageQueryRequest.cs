using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Application.Features.ProductImageFiles.Queries.GetProductImage
{
    public class GetProductImageQueryRequest : IRequest<List<GetProductImageQueryResponse>>
    {
        public string Id { get; set; }
    }
}
