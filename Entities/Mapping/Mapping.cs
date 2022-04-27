using AutoMapper;
using Entities.Concrete;
using Entities.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Mapping
{
    public class Mapping:Profile
    {
        public Mapping()
        {
            CreateMap<User, UserDetailDto>();
            CreateMap<UserDetailDto, User>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();


            CreateMap<UserAddDto, User>();
            CreateMap<User, UserAddDto>();

            CreateMap<User, UserUpdateDto>();
            CreateMap<UserUpdateDto, User>();

            CreateMap<UserDto, UserUpdateDto>();
            CreateMap<UserUpdateDto, UserDto>();
        }
    }
}
