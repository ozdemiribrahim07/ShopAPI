using MediatR;
using Microsoft.AspNetCore.Identity;
using ShopAPI.Application.Abstraction.Services;
using ShopAPI.Application.Abstraction.TokenAbs;
using ShopAPI.Application.Dtos;
using ShopAPI.Application.Dtos.User;
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
        readonly IAuthService _authService;
        readonly IUserService _userService;

        public LoginUserCommandHandler(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
         
            var token = await _authService.LoginAsync(request.EmailOrUsername,request.Password,50);

            return new LoginUserCommandResponse()
            {
                Token = token
            };
        }
    }
}
