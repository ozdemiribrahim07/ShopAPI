using ShopAPI.Application.Dtos.User;
using ShopAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Application.Abstraction.Services
{
    public interface IUserService
    {
        Task<CreateUseResponseDto> CreateAsync(CreateUserDto createUserDto);
        Task UpdateRefreshToken(string refreshToken,DateTime accessTokenExpiration ,AppUser user, int refreshTokenLifeTime);
    }
}
