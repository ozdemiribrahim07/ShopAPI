﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Domain.Entities
{
    public class ProductImageFile : FileBase
    {
        public ICollection<Product> Products { get; set; }
    }
}
