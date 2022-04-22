using Entities.Dtos.UserDtos;
using Microsoft.AspNetCore.Mvc;
using Mvc_UI.ViewModels;
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

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Gender = GenderFill();            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(UserAddViewModel userAddViewModel)
        {
            UserAddDto userAdd = new()
            {
                FirstName = userAddViewModel.FirstName,
                Gender =userAddViewModel.GenderId==1 ?true:false,
                LastName=userAddViewModel.LastName,
                Address=userAddViewModel.Address,
                DateOfBirth=userAddViewModel.DateOfBirth,
                Email=userAddViewModel.Email,
                Password=userAddViewModel.Password,
                UserName=userAddViewModel.UserName
            };

            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync(url + "Users/Add", userAdd);

            if(httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        private List<Gender> GenderFill()
        {
            List<Gender> genders = new();
            genders.Add(new Gender() { Id = 1, GenderName = "Erkek" });
            genders.Add(new Gender() { Id = 2, GenderName = "Kadın" });
            return genders;
        }
        private class Gender
        {
            public int Id { get; set; }
            public string GenderName { get; set; }
        }
    }
}
