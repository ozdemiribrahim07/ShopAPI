using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using ShopAPI.Application.Abstraction.Services;
using ShopAPI.Application.Dtos.User;
using ShopAPI.Application.Features.Users.Commands.CreateUser;
using ShopAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Persistance.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUseResponseDto> CreateAsync(CreateUserDto createUserDto)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = createUserDto.Username,
                Email = createUserDto.Email,
                FullName = createUserDto.FullName,
            }, createUserDto.Password);


            CreateUseResponseDto responseDto = new() { Succeeded = result.Succeeded };
            if (result.Succeeded)
                responseDto.Message = "Kullanıcı başarıyla oluşturulmuştur.";
            else
                foreach (var error in result.Errors)
                    responseDto.Message += $"{error.Code} - {error.Description}<br>";

            return responseDto;
        }


        public async Task UpdateRefreshToken(string refreshToken, DateTime accessTokenExpiration, AppUser user, int refreshTokenLifeTime)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndTime = accessTokenExpiration.AddSeconds(refreshTokenLifeTime);
                await _userManager.UpdateAsync(user);
            }
            else
            {
                throw new NotImplementedException("Token bilgileri güncellenemedi.");
            }
        }
    }
}
