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
        private readonly string url = "http://localhost:53858/api/";

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
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var user = await _httpClient.GetFromJsonAsync<UserDto>(url + "Users/GetById/" + id);

            UserUpdateViewModel userUpdateViewModel = new()
            {
                FirstName = user.FirstName,
                GenderId = user.Gender == true ? 1:2,
                LastName = user.LastName,
                Address = user.Address,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                Password = user.Password,
                UserName = user.UserName
            };
            ViewBag.Gender = GenderFill();

            return View(userUpdateViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id , UserUpdateViewModel userUpdateViewModel)
        {

            UserUpdateDto userUpdateDto = new()
            {
                FirstName = userUpdateViewModel.FirstName,
                Gender = userUpdateViewModel.GenderId == 1 ? true:false,
                LastName = userUpdateViewModel.LastName,
                Address = userUpdateViewModel.Address,
                DateOfBirth = userUpdateViewModel.DateOfBirth,
                Email = userUpdateViewModel.Email,
                Password = userUpdateViewModel.Password,
                UserName = userUpdateViewModel.UserName,
                Id =id
            };
            HttpResponseMessage httpResponseMessage = await _httpClient.PutAsJsonAsync(url + "Users/Update", userUpdateDto);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        } 

       
        public async Task<IActionResult> Delete(int id)
        {
             await _httpClient.DeleteAsync(url + "Users/Delete/" + id);
            
             return RedirectToAction("Index");
        }
        private static List<Gender> GenderFill()
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
