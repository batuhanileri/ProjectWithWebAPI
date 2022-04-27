using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Responses;
using Core.Utilities.Security.Token;
using Core.Utilities.Security.Token.jwt;
using Entities.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenService _tokenService;
        private IMapper _mapper;

        public AuthManager(IUserService userService, ITokenService tokenService, IMapper mapper)
        {
            _userService = userService;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<ApiDataResponse<UserDto>> LoginAsync(UserForLoginDto userForLoginDto)
        {
            var user = await _userService.GetAsync(x => x.UserName == userForLoginDto.UserName && x.Password == userForLoginDto.Password);
            if(user.Data ==null)
            {
                return new ApiErrorDataResponse<UserDto>(null, Messages.UserNotFound);
            }
            else
            {
                if(user.Data.TokenExpireDate==null || string.IsNullOrEmpty(user.Data.Token))
                {
                    return await UpdateToken(user);
                }
                if(user.Data.TokenExpireDate<DateTime.Now)
                {
                    return await UpdateToken(user);

                }
            }
            return new ApiSuccessDataResponse<UserDto>(user.Data, Messages.LoginSuccess);


        }
        private async Task<ApiDataResponse<UserDto>> UpdateToken(ApiDataResponse<UserDto> user)
        {
            var accessToken = _tokenService.CreateToken(user.Data.Id, user.Data.UserName);
            var userUpdate = _mapper.Map<UserUpdateDto>(user.Data);
            userUpdate.Token = accessToken.Token;
            userUpdate.TokenExpireDate = accessToken.Expiration;
            userUpdate.UpdatedUserId = user.Data.Id;
            var result = await _userService.UpdateAsync(userUpdate);
            var userDto = _mapper.Map<UserDto>(result.Data);
            return new ApiSuccessDataResponse<UserDto>(userDto, Messages.LoginSuccess);
        }
    }
}
