using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopAPI.Application.Abstraction.Services;
using ShopAPI.Application.Abstraction.TokenAbs;
using ShopAPI.Application.Dtos;
using ShopAPI.Application.Dtos.User;
using ShopAPI.Application.Features.Users.Commands.LoginUser;
using ShopAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Persistance.Services
{
    public class AuthService : IAuthService
    {

        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenHandler _tokenHandler;
        readonly UserManager<AppUser> _userManager;
        readonly IUserService _userService;

        public AuthService(SignInManager<AppUser> signInManager, ITokenHandler tokenHandler, UserManager<AppUser> userManager, IUserService userService)
        {
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
            _userManager = userManager;
            _userService = userService;
        }


        public async Task<Token> LoginAsync(string emailorusername, string password, int minute)
        {
            AppUser user = await _userManager.FindByNameAsync(emailorusername);

            if (user == null)
            {
                await _userManager.FindByEmailAsync(emailorusername);
            }
            if (user == null)
            {
                throw new Exception("Kullanıcı bulunamadı veya kullanıcı adı veya şifre hatalı !");
            }


            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (result.Succeeded)
            {
                Token token = _tokenHandler.CreateAccessToken(minute,user);
                await _userService.UpdateRefreshToken(token.RefreshToken, token.Expiration, user, 10);
                return token;
            }
            else
            {
                throw new Exception("Kullanıcı bulunamadı veya kullanıcı adı veya şifre hatalı !");
            }
        }

        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            AppUser? appUser = await _userManager.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);

            if (appUser != null && appUser?.RefreshTokenEndTime > DateTime.UtcNow)
            {
                Token token = _tokenHandler.CreateAccessToken(30,appUser);
                await _userService.UpdateRefreshToken(token.RefreshToken, token.Expiration, appUser, 10);
                return token;
            }
            else
            {
                throw new Exception("Refresh token hatalı !");
            }
        }




    }
}
