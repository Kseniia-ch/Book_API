using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BookAPI.Domain.Models;
using BookAPI.Domain.Services;
using BookAPI.Resources;

namespace BookAPI.Controllers
{
    [Route("/api/[controller]")]
    public class AuthController: Controller
    {
        private readonly IAuthService userService;
        private readonly IMapper mapper;
        public AuthController(IAuthService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] SaveUserResource user)
        {
            var userA = await userService.Authenticate(user.Login, user.Password);
            var result = mapper.Map<User, UserResource>(userA);
            var response = new ResponseData
            {
                Success = result != null,
                Message = result != null ? "" : "Incorrect login or password",
                Data = result
            };

            return Ok(response);
        }
    }
}