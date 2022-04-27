using Business.Abstract;
using Entities.Dtos.UserDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> LoginAsync(UserForLoginDto userForLoginDto)
        {
            var result = await _authService.LoginAsync(userForLoginDto);
            if (result.Success)
                return Ok(result);
            else
                return BadRequest();
        }
    }
}
