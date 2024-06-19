using ShopAPI.Application.Dtos;
using ShopAPI.Application.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Application.Abstraction.Services
{
    public interface IAuthService
    {
        Task<Token> LoginAsync(string emailorusername, string password, int minute);
        Task<Token> RefreshTokenLoginAsync(string refreshToken);
    }
}
