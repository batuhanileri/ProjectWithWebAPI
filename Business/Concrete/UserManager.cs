using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
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

        public async Task<bool> DeleteAsync(int id)
        {
            return await _userDal.DeleteAsync(id);
            
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
            var user = await _userDal.GetAsync(x => x.Id == id);
            UserDto userDto = new()
            {
                Address = user.Address,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                Id = user.Id,
                UserName = user.UserName
            };
            return userDto;
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
