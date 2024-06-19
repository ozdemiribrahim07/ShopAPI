using MediatR;
using Microsoft.AspNetCore.Identity;
using ShopAPI.Application.Abstraction.Services;
using ShopAPI.Application.Dtos.User;
using ShopAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShopAPI.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        
        readonly IUserService _userService;

        public CreateUserCommandHandler( IUserService userService)
        {
            _userService = userService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            CreateUseResponseDto createUseResponseDto = await _userService.CreateAsync(new()
            {
                Email = request.Email,
                FullName = request.FullName,
                Password = request.Password,
                Username = request.Username
            });

            return new()
            {
                Message = createUseResponseDto.Message,
                Succeeded = createUseResponseDto.Succeeded
            };
        }
    }
}
