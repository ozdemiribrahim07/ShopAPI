using ShopAPI.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Application.Abstraction.TokenAbs
{
    public interface ITokenHandler
    {
        Token CreateAccessToken(int minute);
    }
}
