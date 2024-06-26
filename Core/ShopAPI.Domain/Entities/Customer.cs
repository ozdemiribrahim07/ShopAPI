﻿using ShopAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Domain.Entities
{
    public class Customer :BaseEntity
    {
        public string CustomerName { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
