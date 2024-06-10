﻿using ShopAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Application.Repositories.FileRepo
{
    public interface IFileWriteRepository : IWriteRepository<FileBase>
    {
    }
}
