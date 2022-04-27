using Core.Utilities.Responses;
using Entities.Dtos.UserDtos;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc_UI.ApiService.Interfaces
{
    public interface IAuthApiService
    {
        Task<ApiDataResponse<UserDto>> LoginAsync(UserForLoginDto loginDto);
    }
}
