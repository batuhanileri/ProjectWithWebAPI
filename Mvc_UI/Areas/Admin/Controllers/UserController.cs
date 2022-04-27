using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mvc_UI.ApiService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc_UI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class UserController : Controller
    {
        private IUserApiService _userApiService;
        private IHttpContextAccessor _httpContextAccessor;

        public UserController(IUserApiService userApiService, IHttpContextAccessor httpContextAccessor)
        {
            _userApiService = userApiService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userApiService.GetListAsync();
            return View(user);
        }
    }
}
