using Entities.Dtos.UserDtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Mvc_UI.Controllers
{
    public class UsersController : Controller
    {
        private readonly HttpClient _httpClient;
        private string url = "http://localhost:53858/api/";

        public UsersController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _httpClient.GetFromJsonAsync<List<UserDetailDto>>(url + "Users/GetList");
            return View(users);
        }
    }
}
