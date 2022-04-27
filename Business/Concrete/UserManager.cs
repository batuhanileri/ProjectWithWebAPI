using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Responses;
using Core.Utilities.Security.Token;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.UserDtos;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;
        public UserManager(IUserDal userDal, IOptions<AppSettings> appSettings,IMapper mapper)
        {
            _userDal = userDal;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        public async Task<ApiDataResponse<UserDto>> AddAsync(UserAddDto userAddDto)
        {
            var user = _mapper.Map<User>(userAddDto);
            //Todo:CreatedDate ve CreatedUserId düzenlenecek
            user.CreatedDate = DateTime.Now;
            user.CreatedUserId = 1;

            var userAdd = await _userDal.AddAsync(user);
            var userDto = _mapper.Map<UserDto>(userAdd);

            return new ApiSuccessDataResponse<UserDto>(userDto,Messages.Added);
        }

        //public async Task<ApiDataResponse<AccessToken>> Authenticate(UserForLoginDto userForLoginDto)
        //{
        //    var user = await _userDal.GetAsync(x => x.UserName == userForLoginDto.UserName && x.Password == userForLoginDto.Password);

        //    if(user ==null)          
        //        return null;

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(_appSettings.SecurityKey);

        //    var tokenDescriptor = new SecurityTokenDescriptor()
        //    {
        //        Subject = new ClaimsIdentity(new[]
        //        {
                    
        //            new Claim(ClaimTypes.Name, user.Id.ToString())
        //        }),
        //        Expires = DateTime.UtcNow.AddDays(7),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };

        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return new AccessToken()
        //    {
        //        Token = tokenHandler.WriteToken(token),
        //        UserName = user.UserName,
        //        Expiration = (DateTime)tokenDescriptor.Expires,
        //        UserId = user.Id
        //    };
        //}

        public async Task<ApiDataResponse<bool>> DeleteAsync(int id)
        {
            var deleted = await _userDal.DeleteAsync(id);
            return new ApiSuccessDataResponse<bool>(deleted, Messages.Deleted);
            
        }

        public async Task<ApiDataResponse<UserDto>> GetAsync(Expression<Func<User, bool>> filter)
        {
            var user = await _userDal.GetAsync(filter);
            if(user !=null)
            {
                var userDto = _mapper.Map<UserDto>(user);
                return new ApiSuccessDataResponse<UserDto>(userDto, Messages.Listed);

            }
            return new ApiErrorDataResponse<UserDto>(null, Messages.NotListed);

        }

        public async Task<ApiDataResponse<UserDto>> GetByIdAsync(int id)
        {
            var user = await _userDal.GetAsync(x => x.Id == id);
            if (user!=null)
            {
                var userDto = _mapper.Map<UserDto>(user);
                return new ApiSuccessDataResponse<UserDto>(userDto, Messages.Listed);
            }
            return new ApiErrorDataResponse<UserDto>(null, Messages.NotListed);
            
        }

        public async Task<ApiDataResponse<IEnumerable<UserDetailDto>>> GetListAsync(Expression<Func<User, bool>> filter = null)
        {
            if(filter==null)
            {
                var response = await _userDal.GetListAsync();
                var userDetail = _mapper.Map<IEnumerable<UserDetailDto>>(response);
                return new ApiSuccessDataResponse<IEnumerable<UserDetailDto>>(userDetail, Messages.Listed);

            }
            else
            {
                var response = await _userDal.GetListAsync(filter);
                var userDetail = _mapper.Map<IEnumerable<UserDetailDto>>(response);
                return new ApiSuccessDataResponse<IEnumerable<UserDetailDto>>(userDetail, Messages.Listed);
            }
          
        }

        public async Task<ApiDataResponse<UserUpdateDto>> UpdateAsync(UserUpdateDto entity)
        {
            var update = await _userDal.GetAsync(x => x.Id == entity.Id);
            var user = _mapper.Map<User>(entity);

            user.CreatedDate = update.CreatedDate;
            user.CreatedUserId = update.CreatedUserId;
            user.UpdatedDate = DateTime.Now;
            user.UpdatedUserId = 1;
            user.Token = entity.Token;
            user.TokenExpireDate = entity.TokenExpireDate;
            var userUpdate = await _userDal.UpdateAsync(user);
            var result = _mapper.Map<UserUpdateDto>(userUpdate);
            return new ApiSuccessDataResponse<UserUpdateDto>(result, Messages.Updated);

        }
    }
}
