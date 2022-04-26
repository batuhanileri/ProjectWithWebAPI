using Business.Abstract;
using Core.Helpers.JWT;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.UserDtos;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        AppSettings _appSettings;

        public UserManager(IUserDal userDal, IOptions<AppSettings> appSettings)
        {
            _userDal = userDal;
            _appSettings = appSettings.Value;
        }

        public async Task<UserDto> AddAsync(UserAddDto entity)
        {
            User user = new()
            {
                Address = entity.Address,
                DateOfBirth = entity.DateOfBirth,
                Email = entity.Email,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Gender = entity.Gender,
                UserName = entity.UserName,
                //Todo:CreatedDate ve CreatedUserId düzenlenecek
                CreatedDate = DateTime.Now,
                CreatedUserId = 1,
                Password = entity.Password
            };

            var userAdd = await _userDal.AddAsync(user);
            UserDto userDto = new()
            {
                Address = userAdd.Address,
                DateOfBirth = userAdd.DateOfBirth,
                Email = userAdd.Email,
                FirstName = userAdd.FirstName,
                LastName = userAdd.LastName,
                Gender = userAdd.Gender,
                UserName = userAdd.UserName
            };

            return userDto;
        }

        public async Task<AccessToken> Authenticate(UserForLoginDto userForLoginDto)
        {
            var user = await _userDal.GetAsync(x => x.UserName == userForLoginDto.UserName && x.Password == userForLoginDto.Password);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.SecuritKey);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            AccessToken accessToken = new()
            {
                Token = tokenHandler.WriteToken(token),
                UserName = user.UserName,
                Expiration = (DateTime)tokenDescriptor.Expires,
                UserId = user.Id
            };
            return await Task.Run(() => accessToken);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _userDal.DeleteAsync(id);
            
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
            var user = await _userDal.GetAsync(x => x.Id == id);
            if (user!=null)
            {
                UserDto userDto = new()
                {
                    Address = user.Address,
                    DateOfBirth = user.DateOfBirth,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender,
                    Id = user.Id,
                    UserName = user.UserName,
                    Password=user.Password
                };
                return userDto;
            }
            return null;
            
        }

        public async Task<IEnumerable<UserDetailDto>> GetListAsync()
        {
            List<UserDetailDto> userDetailDtos = new();
            var response = await _userDal.GetListAsync();
            foreach (var item in response.ToList())
            {
                userDetailDtos.Add(new UserDetailDto()
                {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Gender = item.Gender == true ? "Erkek" : "Kadın",
                    DateOfBirth = item.DateOfBirth,
                    UserName = item.UserName,
                    Address = item.Address,
                    Email = item.Email,
                    Id = item.Id
                });
            }
            return userDetailDtos;
        }

        public async Task<UserUpdateDto> UpdateAsync(UserUpdateDto entity)
        {
            var update = await _userDal.GetAsync(x => x.Id == entity.Id);
            User user = new()
            {
                Address = entity.Address,
                DateOfBirth = entity.DateOfBirth,
                Email = entity.Email,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Gender = entity.Gender,
                UserName = entity.UserName,
                Id = entity.Id,
                CreatedDate = update.CreatedDate,
                CreatedUserId = update.CreatedUserId,
                Password = entity.Password,
                UpdatedDate = DateTime.Now,
                UpdatedUserId = 1
            };
            var userUpdate = await _userDal.UpdateAsync(user);
            UserUpdateDto userUpdateDto = new()
            {
                Address = userUpdate.Address,
                DateOfBirth = userUpdate.DateOfBirth,
                Email = userUpdate.Email,
                FirstName = userUpdate.FirstName,
                LastName = userUpdate.LastName,
                Gender = userUpdate.Gender,
                UserName = userUpdate.UserName,
                Password = userUpdate.Password,
                Id = userUpdate.Id
            };
            return userUpdateDto;
        }
    }
}
