using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Application.Features.ProductImageFiles.Queries.GetProductImage
{
    public class GetProductImageQueryResponse
    {
        public string Path { get; set; }
        public string FileName { get; set; }
        public string Id { get; set; }
    }
}
