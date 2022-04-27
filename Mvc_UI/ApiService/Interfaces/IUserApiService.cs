using Entities.Dtos.UserDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mvc_UI.ApiService.Interfaces
{
    public interface IUserApiService
    {
        Task<List<UserDetailDto>> GetListAsync();
    }
}
