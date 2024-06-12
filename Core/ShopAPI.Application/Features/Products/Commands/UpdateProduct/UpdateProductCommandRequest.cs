﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandRequest : IRequest<UpdateProductCommandResponse>
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public int ProductStock { get; set; }
        public float ProductPrice { get; set; }
    }
}
