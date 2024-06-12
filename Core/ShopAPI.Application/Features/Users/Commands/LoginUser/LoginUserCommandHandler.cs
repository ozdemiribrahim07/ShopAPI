using MediatR;
using Microsoft.AspNetCore.Identity;
using ShopAPI.Application.Abstraction.TokenAbs;
using ShopAPI.Application.Dtos;
using ShopAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Application.Features.Users.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        readonly ITokenHandler _tokenHandler;

        public LoginUserCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {

            AppUser user = await _userManager.FindByNameAsync(request.EmailOrUsername);

            if (user == null)
            {
               await _userManager.FindByEmailAsync(request.EmailOrUsername);
            }
            if (user == null)
            {
                throw new Exception("Kullanıcı bulunamadı veya kullanıcı adı veya şifre hatalı !");
            }


            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (result.Succeeded)
            {
               Token token  = _tokenHandler.CreateAccessToken(5);

                return new LoginUserCommandResponse()
                {
                    Token = token,
                    Message = "Giriş yapıldı"
                };
            }
            else
            {
                return new LoginUserCommandResponse()
                {
                    Message = "Giriş yapılamadı"
                };
            }
        }
    }
}
